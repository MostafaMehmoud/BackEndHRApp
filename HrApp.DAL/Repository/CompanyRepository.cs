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
    public class CompanyRepository: GenericRepository<Company>, ICompanyRepository
    {

        public CompanyRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Company> GetByIdTypeStringAsync(string id)
        {

            return await _dbContext.Companys.FindAsync(id);
        }
        public async Task<bool> UpdateWithSPAsync(Company company)
        {
            try
            {
                var idParam = new OracleParameter("mCNT_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = company.Id
                };

                var nameParam = new OracleParameter("mCNT_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = company.NameAr
                };

                var nameEParam = new OracleParameter("mCNT_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = company.NameEn
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 means update
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDCOMPANIESSP(:mCNT_ID, :mCNT_NAME, :mCNT_NAME_E, :isaddnew, :resultcheck); END;",
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
        public async Task<bool> AddWithSPAsync(Company company)
        {
            try
            {
                var idParam = new OracleParameter("mCNT_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = company.Id
                };

                var nameParam = new OracleParameter("mCNT_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = company.NameAr
                };

                var nameEParam = new OracleParameter("mCNT_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = company.NameEn
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 for insert (0 if you want to update)
                };

                var resultCheckParam = new OracleParameter("resultcheck", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDCOMPANIESSP(:mCNT_ID, :mCNT_NAME, :mCNT_NAME_E, :isaddnew, :resultcheck); END;",
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
