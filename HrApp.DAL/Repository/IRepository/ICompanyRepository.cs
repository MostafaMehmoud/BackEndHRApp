using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface ICompanyRepository: IGenericRepository<Company>
    {
        Task<Company> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Company company);
        Task<bool> UpdateWithSPAsync(Company company);
    }
}
