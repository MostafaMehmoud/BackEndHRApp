using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class UpdateKafil
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string KafilNameAr { get; set; }
        [Required]
        public string KafilNameEn { get; set; }
        [Required]
        public string IdNumber { get; set; }
        [Required]
        public string Mobile { get; set; }
    }
}
