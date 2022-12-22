using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class SundayOfTheYear
    {
        public int Id { get; set; }
        public DateTime FirstSundayOfTheYear { get; set; }
        public DateTime LastDayOfTheYear { get; set; }
        public bool IsCurrentYear { get; set; }
    }
}