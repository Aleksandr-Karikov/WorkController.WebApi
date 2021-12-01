﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Models.BaseModel;

namespace WebApiWorkControllerServer.Models
{
    [Table("Users")]
    public class User:Base
    {
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [EmailAddress]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string Password { get; set; }
        [Column(TypeName = "int")]
        public int? ChiefId { get; set; }
        [ForeignKey("ChiefId")]
        public User Chief { get; set; }
    }
}
