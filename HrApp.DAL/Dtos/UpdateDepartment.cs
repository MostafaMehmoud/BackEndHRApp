using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class UpdateDepartment
    {
        public string Id { get; set; }
        public string DepartmentNameAr { get; set; }
        public string DepartmentNameEn { get; set; }
        public string? ManageId { get; set; }
    }
}
