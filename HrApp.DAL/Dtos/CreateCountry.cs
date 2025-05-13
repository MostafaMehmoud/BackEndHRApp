using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Dtos
{
    public class CreateCountry
    {

        public string NameArCountry { get; set; }
        public string NameEnCountry { get; set; }
        public decimal VATValue { get; set; }
        public string Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public string CurrencyId { get; set; }




    }
}
