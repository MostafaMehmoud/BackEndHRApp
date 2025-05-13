using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class CreateBranch
    {
        public string BranchNameAr { get; set; }
        public string BranchNameEn { get; set; }
        public string? CompanyId { get; set; }
    }
}
