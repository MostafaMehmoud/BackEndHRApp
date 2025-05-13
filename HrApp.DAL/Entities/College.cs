using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Entities
{
    [Table("HR_COLLEGE")]
    public class College
    {
        [Key]
        [Column("COLL_ID")]
        public string? Id { get; set; }
        [Column("COLL_NAME")]
        public string? NameAr { get; set; }
        [Column("COLL_NAME_E")]
        public string? NameEn { get; set; }
    }
}
