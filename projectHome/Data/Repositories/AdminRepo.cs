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
    public class AdminRepo : IAdminRepo
    {
        private readonly IConfiguration _config;

        public AdminRepo(IConfiguration config)
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

        public int AddHomeSlideFile (string filename)
        {
            var returnId = 0;
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = "INSERT INTO HomeSlides (FileName) VALUES (@FileName);" + 
                        "WITH lastId AS(SELECT CAST(SCOPE_IDENTITY() as int) AS lastAdded)" +
                        "UPDATE HomeSlides SET FileName = (CONVERT(varchar, (SELECT lastAdded FROM lastId)) + '_' + @FileName) WHERE ID = (SELECT lastAdded FROM lastId); SELECT CAST(SCOPE_IDENTITY() as int);";
                    conn.Open();
                    var results = conn.QueryMultiple(sql, new { FileName = filename });
                    var result1 = results.Read<int>().First();
                    return result1;
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
            return returnId;
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

        public async Task<int> UpdateHomeSlidesOrder(IEnumerable<int> idList)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "UPDATE HomeSlidesOrder SET HomeSlideID = @First WHERE ID = 1;" +
                        "UPDATE HomeSlidesOrder SET HomeSlideID = @Second WHERE ID = 2" +
                        "UPDATE HomeSlidesOrder SET HomeSlideID = @Third WHERE ID = 3;";
                    conn.Open();
                    var result = await conn.ExecuteAsync(sQuery, new { First = idList.ElementAt(0), Second = idList.ElementAt(1), Third = idList.ElementAt(2) });
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
