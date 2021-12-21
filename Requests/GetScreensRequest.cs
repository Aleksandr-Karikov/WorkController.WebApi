using System;

namespace WorkController.WebApi.Requests
{
    public class GetScreensRequest
    {
       public int UserId { get; set; }
       public DateTime Date { get; set; }
    }
}
