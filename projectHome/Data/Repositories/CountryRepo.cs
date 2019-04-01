using Dapper;
using Microsoft.Extensions.Configuration;
using mor1.Data.IRepositories;
using mor1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.Repositories
{
    public class CountryRepo : ICountryRepo
    {
        private readonly IConfiguration _config;

        public CountryRepo(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString(commonName.defaultConn));
            }
        }

        public IEnumerable<Country> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Countries";
                conn.Open();
                var result = conn.Query<Country>(sQuery);
                return result;
            }
        }
    }
}
