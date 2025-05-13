using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class CreateCity
    {
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public string? CompanyId { get; set; }
    }
}
