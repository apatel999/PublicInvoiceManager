
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class Repository: IDisposable
    {
        protected readonly IDbConnection cnn;
        private string conString;
        public ILogger log;

        public Repository(string conString, ILogger log)
        {
            this.conString = conString;
            this.log = log;
        }

        public IDbConnection Connection {
            get {
                var connection = new MySqlConnection(this.conString);
                connection.Open();
                return connection;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
