using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Entities
{
    [Table("COUNTRY")]
    public class Country
    {
        [Key]
        [Column("COUNID")]
        public string? Id { get; set; }

        [Column("COUNNM")]
        public string? NameAr { get; set; }

        [Column("COUNNM_E")]
        public string? NameEn { get; set; }

        [Column("VATVAL")]
        public decimal? VATValue { get; set; }

        [Column("CURR")]
        public string? Currency { get; set; }

        [Column("CHNG")]
        public decimal? ExchangeRate { get; set; }
        [Column("CURRID")]
        public string? CurrencyId { get; set; }
    }
}
