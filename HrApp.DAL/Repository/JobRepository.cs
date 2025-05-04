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
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddWithSPAsync(Job job)
        {
            try
            {
                var idParam = new OracleParameter("JOB_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = job.Id
                };

                var nameParam = new OracleParameter("JOB_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = job.NameAr
                };

                var nameEParam = new OracleParameter("JOB_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = job.NameEn
                };

                var isAddNewParam = new OracleParameter("ISAddNew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 for insert (0 if you want to update)
                };

                var resultCheckParam = new OracleParameter("Ret", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SAddHRJOBSSP(:JOB_ID, :JOB_NAME, :JOB_NAME_E, :ISAddNew, :Ret); END;",
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

        public async Task<Job> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.Jobs.FindAsync(id);
        }

        public async Task<bool> UpdateWithSPAsync(Job job)
        {
            try
            {
                var idParam = new OracleParameter("JOB_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = job.Id
                };

                var nameParam = new OracleParameter("JOB_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = job.NameAr
                };

                var nameEParam = new OracleParameter("JOB_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = job.NameEn
                };

                var isAddNewParam = new OracleParameter("ISAddNew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 means update
                };

                var resultCheckParam = new OracleParameter("Ret", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                     "BEGIN HR_SAddHRJOBSSP(:JOB_ID, :JOB_NAME, :JOB_NAME_E, :ISAddNew, :Ret); END;",
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
