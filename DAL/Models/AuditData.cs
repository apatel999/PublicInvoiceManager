using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class AuditData
    {
        public DateTime RecordCreateDate { get; set; }
        public DateTime RecordUpdateDate { get; set; }
    }
}
