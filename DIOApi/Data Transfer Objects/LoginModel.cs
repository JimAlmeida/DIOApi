using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DIOApi.Data_Transfer_Objects
{
    [Table("LoginInfo")]
    public class LoginModel
    {
        [Required]
        [Key]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string AccessKey { get; set; }
    }
}
