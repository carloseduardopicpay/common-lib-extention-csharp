using System;
using System.Reflection;

namespace Domain.Entities
{
    public class LogOpenSearch
    {
        public string timestamp_app { get; set; } = DateTime.Now.ToString("MM/dd/yyyy HH:mm").ToString();
        public string log_type { get; set; } = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        public string log_level { get; set; }
        public string thread { get; set; }
        public string service { get; set; }
        public string trace { get; set; }
        public string span { get; set; } = Assembly.GetExecutingAssembly().FullName.Split(',')[1];
        public string @class { get; set; }
        public string message { get; set; }
        public string stack_trace { get; set; }

    }
}
