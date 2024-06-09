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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly GenericDAO<Role> _roleDAO;

        public RoleRepository(GenericDAO<Role> roleDAO) : base(roleDAO)
        {
            _roleDAO = roleDAO;
        }
    }
}
