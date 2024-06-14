using PayrollSystem.Domain.Contracts.Data.Management.Setting;
using PayrollSystem.Domain.Core.Entities.Management.Setting;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using PayrollSystem.Infrastructure.SQL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.SQL.Management.Setting
{
    public class SettingRepository : BaseRepository<SettingModel, int>, ISettingRepository
    {
        ApplicationDbContext _context;
        public SettingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public SettingModel GetDefaultSetting()
        {
            var maxID = _context.Setting.Max(x=>x.ID);
            var data = _context.Setting.Find(maxID);
            if (data != null)
            {
                return data;
            }

            return new SettingModel { ApplicationName = "" , Logo = ""};
        }
    }
}
