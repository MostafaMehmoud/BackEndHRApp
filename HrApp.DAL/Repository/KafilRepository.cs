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
    public class KafilRepository : GenericRepository<Kafil>, IKafilRepository
    {
        private readonly HrAppDbContext _dbContext;

        public KafilRepository(HrAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Kafil> GetByIdTypeStringAsync(string id)
        {
            return await _dbContext.kafils.FindAsync(id);
        }

        public async Task<bool> AddWithSPAsync(Kafil kafil)
        {
            return await SaveKafilSPAsync(kafil, isAddNew: 1); // 1 = Insert
        }

        public async Task<bool> UpdateWithSPAsync(Kafil kafil)
        {
            return await SaveKafilSPAsync(kafil, isAddNew: 0); // 0 = Update
        }

        private async Task<bool> SaveKafilSPAsync(Kafil kafil, int isAddNew)
        {
            var idParam = new OracleParameter("mKAFIL_ID", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = kafil.Id ?? "*"
            };

            var nameArParam = new OracleParameter("mKAFIL_NAME", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = kafil.NameAr
            };

            var nameEnParam = new OracleParameter("mKAFIL_NAME_E", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = kafil.NameEn
            };

            var mobileParam = new OracleParameter("mKAFIL_MOBIL", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = kafil.Mobile
            };

            var idNumParam = new OracleParameter("mKAFIL_IDNUM", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Value = kafil.IdNumber
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
                "BEGIN SADDKAFILSP(:mKAFIL_ID, :mKAFIL_NAME, :mKAFIL_NAME_E, :mKAFIL_MOBIL, :mKAFIL_IDNUM, :isaddnew, :resultcheck); END;",
                idParam, nameArParam, nameEnParam, mobileParam, idNumParam, isAddNewParam, resultParam
            );

            string result = resultParam.Value?.ToString();
            return result?.ToUpper() == "SUCCESS";
        }
    }
}
