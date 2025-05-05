using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class UpdateReligion
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ReligionNameAr { get; set; }
        [Required]
        public string ReligionNameEn { get; set; }
    }
}
