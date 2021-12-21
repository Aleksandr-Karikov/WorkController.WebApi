using System;
using System.Collections.Generic;
using WorkController.WebApi.Requests.Base;

namespace WorkController.WebApi.Requests
{
    public class TimeRequest:BaseRequest
    {
        public int UserId { get; set; }
        public int Milliseconds { get; set; }
        public DateTime DateTime { get; set; }
        public List<byte[]> Screens { get; set; }

    }
}
