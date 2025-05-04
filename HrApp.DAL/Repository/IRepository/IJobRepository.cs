using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<Job> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Job job);
        Task<bool> UpdateWithSPAsync(Job job);
    }
}
