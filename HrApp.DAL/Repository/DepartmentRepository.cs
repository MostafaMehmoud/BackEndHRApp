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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly HrAppDbContext _dbContext;

        public DepartmentRepository(HrAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Department> GetAllInclude()
        {
            return _dbContext.Departments
                .Include(d => d.Manage) // adjust based on your navigation properties
                .ToList();
        }

        public async Task<Department> GetByIdAsync(string id)
        {
            return await _dbContext.Departments.FindAsync(id);
        }

        public async Task<bool> AddWithSPAsync(Department dept)
        {
            return await SaveDepartmentSPAsync(dept, isAddNew: 1);
        }

        public async Task<bool> UpdateWithSPAsync(Department dept)
        {
            return await SaveDepartmentSPAsync(dept, isAddNew: 0);
        }

        private async Task<bool> SaveDepartmentSPAsync(Department dept, int isAddNew)
        {
            var deptIdParam = new OracleParameter("mDept_ID", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = dept.Id ?? "*"
            };

            var nameArParam = new OracleParameter("mDept_NAME", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = dept.NameAr
            };

            var nameEnParam = new OracleParameter("mDept_NAME_E", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = dept.NameEn
            };

            var managerIdParam = new OracleParameter("mMNG_ID", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = dept.ManageId
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
                "BEGIN HR_SADDDEPTSP(:mDept_ID, :mDept_NAME, :mDept_NAME_E, :mMNG_ID, :isaddnew, :resultcheck); END;",
                deptIdParam, nameArParam, nameEnParam, managerIdParam, isAddNewParam, resultParam
            );

            string result = resultParam.Value?.ToString();
            return result?.ToUpper() == "SUCCESS";
        }
    }
}
