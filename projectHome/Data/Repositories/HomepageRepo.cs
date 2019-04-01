using Dapper;
using Microsoft.Extensions.Configuration;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.AdminModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.Repositories
{
    public class HomepageRepo : IHomepageRepo
    {
        private readonly IConfiguration _config;

        public HomepageRepo(IConfiguration config)
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

        public string GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT filename FROM HomeSlides WHERE ID = @ID";
                conn.Open();
                var result = conn.QuerySingle<string>(sQuery, new { ID = id });
                return result;
            }
        }

        public async Task<IEnumerable<HomeSlide>> GetAllSlides()
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = "SELECT hs.ID, hs.Filename, hso.ID AS DisplayOrder FROM HomeSlides AS hs LEFT JOIN HomeSlidesOrder AS hso ON hs.ID = hso.HomeSlideID ORDER BY CASE WHEN hso.ID IS NULL THEN 99999999 END, DisplayOrder;";
                    conn.Open();
                    var result = await conn.QueryAsync<HomeSlide>(sql);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
