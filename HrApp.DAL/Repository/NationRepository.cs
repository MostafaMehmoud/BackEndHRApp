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
    public class NationRepository : GenericRepository<Nation>, INationRepository
    {
        public NationRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Nation> GetByIdTypeStringAsync(string id)
        {

            return await _dbContext.nations.FindAsync(id);
        }
        public async Task<bool> UpdateWithSPAsync(Nation nation)
        {
            try
            {
                var idParam = new OracleParameter("NATION_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = nation.Id
                };

                var nameParam = new OracleParameter("NATION_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = nation.NameAr
                };

                var nameEParam = new OracleParameter("NATION_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = nation.NameEn
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 means update
                };

                var resultCheckParam = new OracleParameter("Ret", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDNATIONSP(:NATION_ID, :NATION_NAME, :NATION_NAME_E, :isaddnew, :Ret); END;",
                    idParam, nameParam, nameEParam, isAddNewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> AddWithSPAsync(Nation nation)
        {
            try
            {
                var idParam = new OracleParameter("NATION_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = nation.Id
                };

                var nameParam = new OracleParameter("NATION_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = nation.NameAr
                };

                var nameEParam = new OracleParameter("NATION_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = nation.NameEn
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 for insert (0 if you want to update)
                };

                var resultCheckParam = new OracleParameter("Ret", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDNATIONSP(:NATION_ID, :NATION_NAME, :NATION_NAME_E, :isaddnew, :Ret); END;",
                    idParam, nameParam, nameEParam, isAddNewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
        }
    }
}
