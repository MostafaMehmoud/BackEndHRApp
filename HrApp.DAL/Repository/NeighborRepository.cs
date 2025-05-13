using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.API.Repos;
using HrApp.DAL.Data;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace HrApp.DAL.Repository
{
    internal class NeighborRepository : GenericRepository<Neighbor>, INeighborRepository
    {
        private readonly HrAppDbContext _dbContext;

        public NeighborRepository(HrAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Neighbor> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.Neighbors.FindAsync(id);
        }

        public async Task<bool> AddWithSPAsync(Neighbor neighbor)
        {
            try
            {
                return await SaveNeighborSPAsync(neighbor, isAddNew: 1); // 1 = Add
            }
            catch
            {
                throw;
            }
           
        }

        public async Task<bool> UpdateWithSPAsync(Neighbor neighbor)
        {
            try
            {
                return await SaveNeighborSPAsync(neighbor, isAddNew: 0); // 0 = Update
            }
            catch
            {
                throw;
            }
          
        }

        private async Task<bool> SaveNeighborSPAsync(Neighbor neighbor, int isAddNew)
        {
            try
            {
                var idParam = new OracleParameter("mNB_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = neighbor.Id
                };

                var nameArParam = new OracleParameter("mNB_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = neighbor.NameAr
                };

                var nameEnParam = new OracleParameter("mNB_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = neighbor.NameEn
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = isAddNew
                };

                var resultParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDNEIGHBORSP(:mNB_ID, :mNB_NAME, :mNB_NAME_E, :isaddnew, :resultcheck); END;",
                    idParam, nameArParam, nameEnParam, isAddNewParam, resultParam
                );

                var result = resultParam.Value?.ToString();
                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
            
        }
    }
}
