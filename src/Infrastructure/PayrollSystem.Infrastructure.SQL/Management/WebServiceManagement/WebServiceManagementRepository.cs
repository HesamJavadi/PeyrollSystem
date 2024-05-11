using PayrollSystem.Domain.Contracts.Data.Management.WebServiceManagement;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using PayrollSystem.Infrastructure.SQL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.SQL.Management.WebServiceManagement
{
    public class WebServiceManagementRepository : BaseRepository<WebServiceManagementModel, int> , IWebServiceManagementRepository
    {
        public WebServiceManagementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
