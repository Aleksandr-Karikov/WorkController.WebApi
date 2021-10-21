using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWorkControllerServer.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column(TypeName = "Guid")]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string MiddleName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Login { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        [Column(TypeName = "Guid")]
        public Guid ChiefId { get; set; }
        [ForeignKey("ChiefId")]
        public User Chief { get; set; }
    }
}
