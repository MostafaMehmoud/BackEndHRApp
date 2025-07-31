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
    public class ReligionRepository : GenericRepository<Religion>, IReligionRepository
    {
        public ReligionRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddWithSPAsync(Religion religion)
        {
            try
            {
                var idParam = new OracleParameter("MRELIG_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = religion.Id
                };

                var nameParam = new OracleParameter("MRELIG_NAME ", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = religion.NameAr
                };

                var nameEParam = new OracleParameter("MRELIG_NAME_E ", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = religion.NameEn
                };

                var ISADDNEWParam = new OracleParameter("ISADDNEW", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 for insert (0 if you want to update)
                };

                var resultCheckParam = new OracleParameter("resultcheck ", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDRELIGIONSP(:MRELIG_ID, :MRELIG_NAME, :MRELIG_NAME_E, :ISADDNEW, :resultcheck); END;",
                    idParam, nameParam, nameEParam, ISADDNEWParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
                
            }
        }

        public async Task<Religion> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.religions.FindAsync(id);
        }

        public async Task<bool> UpdateWithSPAsync(Religion religion)
        {
            try
            {
                var idParam = new OracleParameter("MRELIG_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = religion.Id
                };

                var nameParam = new OracleParameter("MRELIG_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = religion.NameAr
                };

                var nameEParam = new OracleParameter("MRELIG_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = religion.NameEn
                };

                var ISADDNEWParam = new OracleParameter("ISADDNEW", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 means update
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDRELIGIONSP(:MRELIG_ID, :MRELIG_NAME, :MRELIG_NAME_E, :ISADDNEW, :resultcheck ); END;",
                    idParam, nameParam, nameEParam, ISADDNEWParam, resultCheckParam
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
