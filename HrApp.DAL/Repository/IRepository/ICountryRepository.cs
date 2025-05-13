using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IEnumerable<Country> GetAllInclude();
        Task<Country> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Country country);
        Task<bool> UpdateWithSPAsync(Country country);
    }
}
