using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface INationRepository : IGenericRepository<Nation>
    {
        Task<Nation> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Nation nation);
        Task<bool> UpdateWithSPAsync(Nation nation);
    }
}
