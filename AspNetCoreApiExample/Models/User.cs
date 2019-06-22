using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Models
{
    [Table("users")]
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string AccessKey { get; set; }

    }
}
