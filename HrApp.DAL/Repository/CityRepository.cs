
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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {

        public CityRepository(HrAppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<City> GetByIdTypeStringAsync(string id)
        {

            return await _dbContext.Cities.FindAsync(id);
        }
        public async Task<bool> UpdateWithSPAsync(City City)
        {
            try
            {
                var idParam = new OracleParameter("mCITY_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.Id
                };

                var nameParam = new OracleParameter("mCITY_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.NameAr
                };

                var nameEParam = new OracleParameter("mCITY_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.NameEn
                };

                var companyIdParam = new OracleParameter("mCNT_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.CompanyId // Make sure this is available in your City model
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 0 // 0 = update
                };

                var resultCheckParam = new OracleParameter("resultcheck ", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SAddCITYSP(:mCITY_ID, :mCITY_NAME, :mCITY_NAME_E, :mCNT_ID, :isaddnew, :resultcheck ); END;",
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
        public async Task<bool> AddWithSPAsync(City City)
        {
            try
            {
                var idParam = new OracleParameter("mCITY_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.Id
                };

                var nameParam = new OracleParameter("mCITY_NAME", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.NameAr
                };

                var nameEParam = new OracleParameter("mCITY_NAME_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.NameEn
                };

                var companyIdParam = new OracleParameter("mCNT_ID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = City.CompanyId // make sure this exists
                };

                var isAddNewParam = new OracleParameter("isaddnew", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1 // 1 = insert
                };

                var resultCheckParam = new OracleParameter("resultcheck ", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDCitySP(:mCITY_ID, :mCITY_NAME, :mCITY_NAME_E, :mCNT_ID, :isaddnew, :resultcheck ); END;",
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
        public IEnumerable<City> GetAllInclude()
        {
            return _dbContext.Cities
                .Include(b => b.company)
                .ToList();
        }



    }
}
