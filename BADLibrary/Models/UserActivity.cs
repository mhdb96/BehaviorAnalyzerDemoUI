using System;
using System.Collections.Generic;
using System.Text;

namespace BADLibrary.Models
{
    public class UserActivity
    {
        public string ActivityType { get; set; }
        public DateTime DateTime { get; set; }
        public string Label{ 
            get 
            {
                return $"{ActivityType} on {DateTime}";
            } 
        }
    }
}
