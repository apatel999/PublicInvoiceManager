using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DatabaseOptions:IDatabaseOptions
    {
        private string _connectionString;

        public DatabaseOptions()
        {

        }
        public DatabaseOptions(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public string ConnectionString {
            get { return this._connectionString; }
            set { this._connectionString = value; }
        }
    }
}
