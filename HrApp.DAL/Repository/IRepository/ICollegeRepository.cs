using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface ICollegeRepository : IGenericRepository<College>
    {
        Task<College> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(College college);
        Task<bool> UpdateWithSPAsync(College college);
    }
}
