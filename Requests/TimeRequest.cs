using System;

namespace WorkController.WebApi.Requests
{
    public class TimeRequest
    {
        public int UserId { get; set; }
        public int Milliseconds { get; set; }
        public DateTime DateTime { get; set; }

    }
}
