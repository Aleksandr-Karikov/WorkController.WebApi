using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Models;
using WebApiWorkControllerServer.Models.BaseModel;

namespace WorkController.WebApi.DataBase.Models
{
    [Table("Employes")]
    public class AllowsEmployee:Base
    {
        public int? ChiefId { get; set; }
        [ForeignKey("ChiefId")]
        public User Chief { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
    }
}
