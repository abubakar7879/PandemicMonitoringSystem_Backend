using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PandemicMonitoringSystem.Abstraction;

namespace PandemicMonitoringSystem.Data
{
    public class SqlDatabaseStrategy : IDbStratrgy

    {
        public DbConnection Connection
        {
            get
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json")
                    .Build();
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionString);
                    _connection.Open();
                }
                else if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }
        private DbConnection _connection;
        public void SeedDatabase()
        {

        }
    }
}
