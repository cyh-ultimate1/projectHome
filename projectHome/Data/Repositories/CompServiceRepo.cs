using Dapper;
using Microsoft.AspNetCore.Http;
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
    public class CompServiceRepo : ICompServiceRepo
    {
        private readonly IConfiguration _config;

        public CompServiceRepo(IConfiguration config)
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

        #region Services

        public IEnumerable<Service> GetAllServices()
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT * FROM Services";
                    conn.Open();
                    var result = conn.Query<Service>(sQuery);
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

        public IEnumerable<Service> GetServicesListById(string[] id)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT * FROM Services WHERE ID IN @IdList";
                    conn.Open();
                    var result = conn.Query<Service>(sQuery, new { IdList = id });
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

        #endregion


        #region Category

        public IEnumerable<ServiceCategory> GetAllCat()
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT * FROM ServiceCategories";
                    conn.Open();
                    var result = conn.Query<ServiceCategory>(sQuery);
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

        #endregion

        #region QuoteForm

        public int AddQuoteForm(ReqQuoteForm quote)
        {
            var returnId = 0;
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = "INSERT INTO ReqQuoteForm (" + 
                        nameof(quote.CountryID) + ", PropertyTypeID, PropertyStatusID, AreaSize, RenovBudget, KeyCollectionPeriod, LoanRequired, Style, Comment, FullName, ContactNum, Email, Gender, Address) VALUES (@CountryID, @PropertyTypeID, @PropertyStatusID, @AreaSize, @RenovBudget, @KeyCollectionPeriod, @LoanRequired, @Style, @Comment, @FullName, @ContactNum, @Email, @Gender, @Address);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";
                    conn.Open();
                    returnId = conn.Query<int>(sql, new { CountryID = quote.CountryID,
                        PropertyTypeID = quote.PropertyTypeID,
                        PropertyStatusID = quote.PropertyStatusID,
                        AreaSize = quote.AreaSize,
                        RenovBudget = quote.RenovBudget,
                        KeyCollectionPeriod = quote.KeyCollectionPeriod,
                        LoanRequired = quote.LoanRequired,
                        Style = quote.Style,
                        Comment = quote.Comment,
                        FullName = quote.FullName,
                        ContactNum = quote.ContactNum,
                        Email = quote.Email,
                        Gender = quote.Gender,
                        Address = quote.Address
                    }).Single();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                return returnId;
            }
        }

        public int AddSelectedService(int quoteFormId, int serviceId)
        {
            var returnId = 0;
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = "INSERT INTO SelectedServices (ReqQuoteFormID, ServiceID) VALUES (@ReqQuoteFormID, @ServiceID)";
                    conn.Open();
                    returnId = conn.Execute(sql, new { ReqQuoteFormID = quoteFormId, ServiceID = serviceId });
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

        public int AddFloorPlanFile(string filename, string filepath, int quoteFormId)
        {
            var returnId = 0;
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = "INSERT INTO FloorPlanFiles (FileName, FilePath, reqQuoteFormId) VALUES (@FileName, @FilePath, @reqQuoteFormId)";
                    conn.Open();
                    returnId = conn.Execute(sql, new { FileName = filename, FilePath = filepath, reqQuoteFormId = quoteFormId });
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

        public async Task<IEnumerable<ReqQuoteForm>> GetReqQuoteForms()
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = "SELECT * FROM ReqQuoteForm";
                    conn.Open();
                    var results = await conn.QueryAsync<ReqQuoteForm>(sql);
                    return results;
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

        #endregion

    }
}

/*[ID],[CountryID],[PropertyTypeID],[PropertyStatusID],[AreaSize],[RenovBudget],[KeyCollectionPeriod],[LoanRequired],[Style],[Comment],[FullName],*//*[ContactNum],[Email],[Gender],[Address] */