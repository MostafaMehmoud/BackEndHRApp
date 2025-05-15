using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository
{
    public interface IKafilRepository : IGenericRepository<Kafil>
    {
        Task<Kafil> GetByIdTypeStringAsync(string id);
        Task<bool> AddWithSPAsync(Kafil Kafil);
        Task<bool> UpdateWithSPAsync(Kafil Kafil);
    }
}
