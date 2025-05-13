using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Entities
{
    [Table("HR_NEIGHBOR")]
    public class Neighbor
    {
        [Key]
        [Column("NB_ID")]
        public string? Id { get; set; }

        [Column("NB_NAME")]
        public string? NameAr { get; set; }

        [Column("NB_NAME_E")]
        public string? NameEn { get; set; }

        [Column("TYPE")]
        public int? Type { get; set; }
    }
}
