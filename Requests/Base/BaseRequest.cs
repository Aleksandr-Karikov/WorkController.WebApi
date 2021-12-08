using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkController.WebApi.Requests.Base
{
    public class BaseRequest
    {
        public bool IsAdmin { get; set; }
        public string Error{ get; set; }
    }
}
