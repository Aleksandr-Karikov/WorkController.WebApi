using System.ComponentModel.DataAnnotations.Schema;
using WorkController.WebApi.DataBase.Models.BaseModel;

namespace WorkController.WebApi.DataBase.Models
{
    public class ScreenShots:Base
    {
        public byte[] Screen { get; set; }
        public int TimeId { get; set; }

        [ForeignKey("TimeId")]
        public Time Time { get; set; }
    }
}
