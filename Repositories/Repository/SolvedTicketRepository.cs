using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
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

        public void PurchaseTickets(List<Ticket> tickets, Account account, int quantity)//promotion, quantity of ticket, transaction - transaction hítory
        {

            foreach (var ticket in tickets)
            {
                var promotion = new Promotion();
                promotion = _promotionRepository.CheckDiscount(quantity);

                var databaseTicket = _unitOfWork.TicketDAO.GetById(ticket.Id);
                if (databaseTicket == null) throw new Exception("NOT FOUND!");
                if (databaseTicket.Quantity > 0)
                {
                    if (ticket.Status == 0) continue;
                    if ((double)quantity! * (double)databaseTicket.Price! > account.Wallet) throw new Exception("YOUR PRICE IN ACCOUNT NOT ENOUGH");
                    if ((double)quantity * (double)databaseTicket.Price <= account.Wallet)
                        _accountRepository.MinusDebt(quantity, databaseTicket.Price, promotion.Discount, account);

                    databaseTicket.Quantity = databaseTicket.Quantity - quantity;
                    if (databaseTicket.Quantity == 0) databaseTicket.Status = 0;

                    var checkSoldTicket = _unitOfWork.SolvedTicketDAO.GetAll().Count();
                    var totalPrice = (int?)((double)databaseTicket.Price! * (double)quantity! - (double)databaseTicket.Price * (double)quantity * promotion!.Discount);
                    var solvedTicket = new SolvedTicket
                    {
                        Id = checkSoldTicket + 1,
                        AccountId = account.Id,
                        TicketId = ticket.Id,
                        Quantity = quantity,
                        TotalPrice = totalPrice,
                        PromotionId = promotion!.Id,
                    };
                    var yourSolvedTicket = _unitOfWork.SolvedTicketDAO.Add(solvedTicket);
                    _unitOfWork.TicketDAO.Update(databaseTicket);
                    _unitOfWork.SaveChanges();

                    var checkTransaction = _unitOfWork.TransactionDAO.GetAll().Count();
                    var transaction = new Transaction
                    {
                        Id = checkTransaction + 1,
                        EventId = databaseTicket.EventId,
                        SolvedTicketId = yourSolvedTicket.Id,
                        TypeId = 1,
                        Status = "Completed"
                    };
                    _unitOfWork.TransactionDAO.Add(transaction);
                    _unitOfWork.SaveChanges();

                    var checkTransactionHistory = _unitOfWork.TransactionHistoryDAO.GetAll().Count();
                    var transactionHistory = new TransactionHistory
                    {
                        Id = checkTransactionHistory + 1,
                        TransactionId = transaction.Id,
                        Price = totalPrice,
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

        public Boolean CheckSolvedTicket(int ticketId)
        {
            var checksolvedTicket = _unitOfWork.SolvedTicketDAO.Find(a => a.TicketId == ticketId);
            if (checksolvedTicket.IsNullOrEmpty())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
