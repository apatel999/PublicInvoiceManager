using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IDatabaseOptions
    {
        string ConnectionString { get; set; }
    }
}
