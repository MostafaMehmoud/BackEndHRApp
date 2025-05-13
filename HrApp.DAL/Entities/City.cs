using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Entities
{
    [Table("HR_CITY")]
    public class City
    {
        public City()
        {
            company = new Company();
        }
        [Key]
        [Column("CITY_ID")]
        public string? Id { get; set; }
        [Column("CITY_NAME")]
        public string? NameAr { get; set; }
        [Column("CITY_NAME_E")]
        public string? NameEn { get; set; }
        [Column("CNT_ID")]
        [ForeignKey("CNT_ID")]
        public string? CompanyId { get; set; }
        public virtual Company company { get; set; }
    }
}
