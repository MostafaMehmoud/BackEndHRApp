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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HrApp.DAL.Repository
{
    public class ManageRepository : GenericRepository<Manage>, IManageRepository
    {
        public ManageRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddWithSPAsync(Manage manage)
        {
            try
            {
                var idParam = new OracleParameter("MNG_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = manage.Id
                };

                var nameParam = new OracleParameter("MNG_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = manage.NameAr
                };

                var nameEParam = new OracleParameter("MNG_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = manage.NameEn
                };

                var isAddNewParam = new OracleParameter("ISAddNew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 for insert (0 if you want to update)
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SAddMANAGESP(:MNG_ID, :MNG_NAME, :MNG_NAME_E, :ISAddNew, :resultcheck); END;",
                    idParam, nameParam, nameEParam, isAddNewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }

        public async Task<Manage> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.Manages.FindAsync(id);
        }

        public async Task<bool> UpdateWithSPAsync(Manage manage)
        {
            try
            {
                var idParam = new OracleParameter("MNG_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = manage.Id
                };

                var nameParam = new OracleParameter("MNG_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = manage.NameAr
                };

                var nameEParam = new OracleParameter("MNG_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = manage.NameEn
                };

                var isAddNewParam = new OracleParameter("ISAddNew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 means update
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SAddMANAGESP(:MNG_ID, :MNG_NAME, :MNG_NAME_E, :ISAddNew, :resultcheck); END;",
                    idParam, nameParam, nameEParam, isAddNewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }
    }
}
