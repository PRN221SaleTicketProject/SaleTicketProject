using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class SolvedTicketRepository : GenericRepository<SolvedTicket>, ISolvedTicketRepository
    {
        private readonly GenericDAO<SolvedTicket> _solvedTicketDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IAccountRepository _accountRepository;

        public SolvedTicketRepository(GenericDAO<SolvedTicket> solvedTicketDAO, IUnitOfWork unitOfWork, IPromotionRepository promotionRepository, IAccountRepository accountRepository) : base(solvedTicketDAO)
        {
            _solvedTicketDAO = solvedTicketDAO;
            _unitOfWork = unitOfWork;
            _promotionRepository = promotionRepository;
            _accountRepository = accountRepository;
        }

        public void PurchaseTickets(List<Ticket> tickets, Account account, TransactionType type)//promotion, quantity of ticket, transaction - transaction hítory
        {

            foreach (var ticket in tickets)
            {
                var promotion = new Promotion();
                promotion = _promotionRepository.CheckDiscount(ticket.Quantity);

                var databaseTicket = _unitOfWork.TicketDAO.GetById(ticket.Id);
                if (databaseTicket == null) throw new Exception("NOT FOUND!");
                if (databaseTicket.Quantity > 0)
                {
                    if (ticket.Status == 0) continue;
                    if ((double)ticket.Quantity! * (double)databaseTicket.Price! > account.Wallet) throw new Exception("YOUR PRICE IN ACCOUNT NOT ENOUGH");
                    if ((double)ticket.Quantity * (double)databaseTicket.Price <= account.Wallet) 
                        _accountRepository.MinusDebt(ticket.Quantity, databaseTicket.Price, promotion.Discount, account);

                    databaseTicket.Quantity = databaseTicket.Quantity - ticket.Quantity;
                    if (databaseTicket.Quantity == 0) databaseTicket.Status = 0;

                    var solvedTicket = new SolvedTicket
                    {
                        AccountId = account.Id,
                        TicketId = ticket.Id,
                        Quantity = ticket.Quantity,
                        TotalPrice = (int?)((double)databaseTicket.Price! * (double)ticket.Quantity! - (double)databaseTicket.Price * (double)ticket.Quantity * promotion!.Discount),
                        PromotionId = promotion!.Id,
                    };
                    var yourSolvedTicket = _unitOfWork.SolvedTicketDAO.Add(solvedTicket);
                    _unitOfWork.TicketDAO.Update(databaseTicket);
                    _unitOfWork.SaveChanges();

                    var transaction = new Transaction
                    {
                        EventId = databaseTicket.EventId,
                        SolvedTicketId = yourSolvedTicket.Id,
                        TypeId = type.Id,
                        Status = "Completed"
                    };
                    _unitOfWork.TransactionDAO.Add(transaction);
                    _unitOfWork.SaveChanges();

                    var transactionHistory = new TransactionHistory
                    {
                        TransactionId = transaction.Id,
                        Price = ticket.Price,
                        Time = DateOnly.FromDateTime(DateTime.Now),
                        Status = "Completed"
                    };
                    _unitOfWork.TransactionHistoryDAO.Add(transactionHistory);
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public List<SolvedTicket> GetSolvedTicketsByAccountId(int accountId)
        {
            return _unitOfWork.SolvedTicketDAO.Find(a => a.AccountId == accountId).ToList();
        }
    }
}
