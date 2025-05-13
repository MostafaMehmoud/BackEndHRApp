using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class UpdateBranch
    {
        public string Id { get; set; }
        public string BranchNameAr { get; set; }
        public string BranchNameEn { get; set; }
        public string? CompanyId { get; set; }
    }
}
