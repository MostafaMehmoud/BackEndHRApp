using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface IManageRepository : IGenericRepository<Manage>
    {
        Task<Manage> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Manage manage);
        Task<bool> UpdateWithSPAsync(Manage manage);
    }
}
