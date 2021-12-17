using System;
using System.ComponentModel.DataAnnotations.Schema;
using WorkController.WebApi.DataBase.Models.BaseModel;

namespace WorkController.WebApi.DataBase.Models
{
    public class Time:Base
    {

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int Milleseconds { get; set; } 

    }
}
