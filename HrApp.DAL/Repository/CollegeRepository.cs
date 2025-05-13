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
    public class CollegeRepository : GenericRepository<College>, ICollegeRepository
    {
        public CollegeRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddWithSPAsync(College College)
        {
            try
            {
                var idParam = new OracleParameter("mCOLL_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = College.Id
                };

                var nameParam = new OracleParameter("mCOLL_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = College.NameAr
                };

                var nameEParam = new OracleParameter("mCOLL_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = College.NameEn
                };

                var isaddnewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 for insert (0 if you want to update)
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDCOLLEGESP(:mCOLL_ID, :mCOLL_NAME, :mCOLL_NAME_E, :isaddnew, :resultcheck); END;",
                    idParam, nameParam, nameEParam, isaddnewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
        }

        public async Task<College> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.Colleges.FindAsync(id);
        }

        public async Task<bool> UpdateWithSPAsync(College College)
        {
            try
            {
                var idParam = new OracleParameter("mCOLL_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = College.Id
                };

                var nameParam = new OracleParameter("mCOLL_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = College.NameAr
                };

                var nameEParam = new OracleParameter("mCOLL_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = College.NameEn
                };

                var isaddnewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 means update
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                     "BEGIN HR_SADDCOLLEGESP(:mCOLL_ID, :mCOLL_NAME, :mCOLL_NAME_E, :isaddnew, :resultcheck); END;",
                    idParam, nameParam, nameEParam, isaddnewParam, resultCheckParam
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
