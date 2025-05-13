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
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HrAppDbContext _dbContext;

        public CountryRepository(HrAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Country> GetAllInclude()
        {
            return _dbContext.Countries.ToList();
        }

        public async Task<Country> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.Countries.FindAsync(id);
        }

        public async Task<bool> AddWithSPAsync(Country country)
        {
            return await SaveCountrySPAsync(country, isAddNew: 1);
        }

        public async Task<bool> UpdateWithSPAsync(Country country)
        {
            return await SaveCountrySPAsync(country, isAddNew: 0);
        }

        private async Task<bool> SaveCountrySPAsync(Country country, int isAddNew)
        {
            try
            {
                var idParam = new OracleParameter("MCOUNID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.Id
                };

                var nameArParam = new OracleParameter("MCOUNNM", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.NameAr
                };

                var nameEnParam = new OracleParameter("MCOUNNM_E", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.NameEn
                };

                var vatValParam = new OracleParameter("MVATVAL", OracleDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.VATValue ?? 0
                };

                var currIdParam = new OracleParameter("MCURRID", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.CurrencyId
                };

                var currParam = new OracleParameter("MCURR", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.Currency
                };

                var chngParam = new OracleParameter("MCHNG", OracleDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = country.ExchangeRate ?? 0
                };

                var isAddNewParam = new OracleParameter("ISADDNEW", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = isAddNew
                };

                var resultParam = new OracleParameter("RESULTCHECK", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "BEGIN HR_SADDCOUNTRYSP(:MCOUNID, :MCOUNNM, :MCOUNNM_E, :MVATVAL, :MCURRID, :MCURR, :MCHNG, :ISADDNEW, :RESULTCHECK); END;",
                    idParam, nameArParam, nameEnParam, vatValParam,
                    currIdParam, currParam, chngParam, isAddNewParam, resultParam
                );

                string result = resultParam.Value?.ToString();
                return result?.ToUpper() == "SUCCESS";
            }
            catch
            {
                throw;
            }
         }   
    }

}
