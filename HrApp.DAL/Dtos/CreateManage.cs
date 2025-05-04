using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class CreateManage
    {

        [Required]
        public string ManageNameAr { get; set; }
        [Required]
        public string ManageNameEn { get; set; }
    }
}
