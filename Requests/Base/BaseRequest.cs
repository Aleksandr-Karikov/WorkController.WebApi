using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkController.WebApi.Requests.Base
{
    public class BaseRequest
    {
        public bool IsAdmin { get; set; }
        public List<string> ErrorList { get; private set; }
        internal void SetErrorMessege(string error)
        {
            if (ErrorList == null)
            {
                ErrorList = new();
            }
            ErrorList.Add(error);
        }
    }
}
