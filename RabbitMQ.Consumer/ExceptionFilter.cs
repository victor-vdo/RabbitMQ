using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class ExceptionFilter
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
