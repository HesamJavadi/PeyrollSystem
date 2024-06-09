using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.Common
{
    public class PageInfoRequest
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string? sort { get; set; }
        public int total { get; set; }
        public string? query { get; set; }
    }
}
