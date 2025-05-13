using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface ICityRepository : IGenericRepository<City>
    {
        IEnumerable<City> GetAllInclude();
        Task<City> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(City city);
        Task<bool> UpdateWithSPAsync(City city);
    }
}
