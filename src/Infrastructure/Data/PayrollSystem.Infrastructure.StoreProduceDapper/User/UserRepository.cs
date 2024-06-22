using Dapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Domain.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.Dapper.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IAuthService _authService;
        public UserRepository(IDbConnection dbConnection, IAuthService authService)
        {
            _dbConnection = dbConnection;
            _authService = authService;
        }

        public async Task<List<UserPersonnelModel>> GetUsers()
        {
            string sqlQuery = @"
                    SELECT 
       PayrollDB.dbo.TblPsl.FirstName, 
       PayrollDB.dbo.TblPsl.LastName, 
       PayrollDB.dbo.TblPsl.PersonelCode, 
       PayrollDB.dbo.TblPsl.NationalCode, 
       PayrollDB.dbo.TblPsl.IsActive,
       PayrollDB.dbo.TblPsl.Phone
   FROM PayrollSystem.ps.AspNetUsers as tblUser 
   RIGHT OUTER JOIN PayrollDB.dbo.TblPsl 
       ON tblUser.pepCode = PayrollDB.dbo.TblPsl.PersonelCode
   WHERE PayrollDB.dbo.TblPsl.IsActive = 1 
       AND tblUser.pepCode IS NULL";

            IEnumerable<UserPersonnelModel> userPersonnels = await _dbConnection.QueryAsync<UserPersonnelModel>(sqlQuery);
            return userPersonnels.ToList();
        }
    }
}
