using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class CreateNeighbor
    {
        [Required]
        public string NeighborNameAr { get; set; }
        [Required]
        public string NeighborNameEn { get; set; }
    }
}
