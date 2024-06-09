using BusinessObjects;
using DataAccessLayers;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly GenericDAO<Promotion> _promotionDAO;

        public PromotionRepository(GenericDAO<Promotion> promotionDAO) : base(promotionDAO)
        {
            _promotionDAO = promotionDAO;
        }
    }
}
