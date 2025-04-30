using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrApp.DAL.Entities
{
    [Table("PW")]
    public class User
    {
        [Column("USER_ID")]
        public string? UserId { get; set; }
        [Column("USER_NAME")]
        public string? Username { get; set; }
        [Column("PW1")]
        public string? Password { get; set; }
        [Column("ADMN")]
        public int? Admin { get; set; }
        [Column("EMP_ID")]
        public string? EmployeeId { get; set; }
       
    }
}
