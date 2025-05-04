using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class UpdateJob
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string JobNameAr { get; set; }
        [Required]
        public string JobNameEn { get; set; }
    }
}
