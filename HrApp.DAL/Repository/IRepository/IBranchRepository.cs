using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface IBranchRepository : IGenericRepository<Branch>
    {
        IEnumerable<Branch> GetAllInclude();   
        Task<Branch> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Branch branch);
        Task<bool> UpdateWithSPAsync(Branch branch);
    }
}
