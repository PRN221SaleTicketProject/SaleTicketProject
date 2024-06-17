using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly GenericDAO<Promotion> _promotionDAO;
        private readonly IUnitOfWork _unitOfWork;

        public PromotionRepository(GenericDAO<Promotion> promotionDAO, IUnitOfWork unitOfWork) : base(promotionDAO)
        {
            _promotionDAO = promotionDAO;
            _unitOfWork = unitOfWork;
        }

        public Promotion CheckDiscount(int? quantity)
        {
            var promotion = new Promotion();
            if (quantity >= 10 && quantity < 20) promotion = _unitOfWork.PromotionDAO.FindOne(a => a.Condition == 10);

            if (quantity >= 20) promotion = _unitOfWork.PromotionDAO.FindOne(a => a.Condition == 20);
            if (promotion == null) throw new Exception("no promotion was found");
            return promotion;
        }
    }
}
