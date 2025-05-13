
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface INeighborRepository : IGenericRepository<Neighbor>
    {
        Task<Neighbor> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Neighbor Neighbor);
        Task<bool> UpdateWithSPAsync(Neighbor Neighbor);
    }
}
