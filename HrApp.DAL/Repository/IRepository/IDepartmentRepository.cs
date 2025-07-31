using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Department> GetAllInclude();
        Task<Department> GetByIdAsync(string id);
        Task<bool> AddWithSPAsync(Department dept);
        Task<bool> UpdateWithSPAsync(Department dept);
    }
}
