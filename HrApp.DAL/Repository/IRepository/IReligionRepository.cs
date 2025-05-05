using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface IReligionRepository : IGenericRepository<Religion>
    {
        Task<Religion> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Religion religion);
        Task<bool> UpdateWithSPAsync(Religion religion);
    }
}
