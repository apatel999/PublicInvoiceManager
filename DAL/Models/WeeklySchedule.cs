using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class WeeklySchedule
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string RouteNumber { get; set; }
        public string  Description { get; set; }
        public int Week { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int SundayOfTheYearId { get; set; }
    }
}
