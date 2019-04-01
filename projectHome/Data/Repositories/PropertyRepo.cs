using Dapper;
using Microsoft.Extensions.Configuration;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.ReqQuoteModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.Repositories
{
    public class PropertyRepo : IPropertyRepo
    {
        private readonly IConfiguration _config;

        public PropertyRepo(IConfiguration config)
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

        public IEnumerable<PropertyType> GetAllPropertyTypes()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM PropertyTypes";
                conn.Open();
                var result = conn.Query<PropertyType>(sQuery);
                return result;
            }
        }

        public IEnumerable<PropertyStatus> GetAllPropertyStatus()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM PropertyStatus";
                conn.Open();
                var result = conn.Query<PropertyStatus>(sQuery);
                return result;
            }
        }

    }
}
