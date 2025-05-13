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
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {

        public BranchRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Branch> GetByIdTypeStringAsync(string id)
        {

            return await _dbContext.Branches.FindAsync(id);
        }
        public async Task<bool> UpdateWithSPAsync(Branch branch)
        {
            try
            {
                var idParam = new OracleParameter("BRANCH_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.Id
                };

                var nameParam = new OracleParameter("BRANCH_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.NameAr
                };

                var nameEParam = new OracleParameter("BRANCH_NAME_e", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.NameEn
                };

                var companyIdParam = new OracleParameter("CNT_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.CompanyId // Make sure this is available in your Branch model
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 = update
                };

                var resultCheckParam = new OracleParameter("Ret", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDBRANCHSP(:BRANCH_ID, :BRANCH_NAME, :BRANCH_NAME_e, :CNT_ID, :isaddnew, :Ret); END;",
                    idParam, nameParam, nameEParam, companyIdParam, isAddNewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> AddWithSPAsync(Branch branch)
        {
            try
            {
                var idParam = new OracleParameter("BRANCH_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.Id
                };

                var nameParam = new OracleParameter("BRANCH_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.NameAr
                };

                var nameEParam = new OracleParameter("BRANCH_NAME_e", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.NameEn
                };

                var companyIdParam = new OracleParameter("CNT_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = branch.CompanyId // make sure this exists
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 = insert
                };

                var resultCheckParam = new OracleParameter("Ret", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDBRANCHSP(:BRANCH_ID, :BRANCH_NAME, :BRANCH_NAME_e, :CNT_ID, :isaddnew, :Ret); END;",
                    idParam, nameParam, nameEParam, companyIdParam, isAddNewParam, resultCheckParam
                );

                string result = resultCheckParam.Value?.ToString();

                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Branch> GetAllInclude()
        {
            return _dbContext.Branches
                .Include(b => b.company)
                .ToList();
        }



    }
}
