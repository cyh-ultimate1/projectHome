using Dapper;
using Microsoft.Extensions.Configuration;
using mor1.Data.IRepositories;
using mor1.Models;
using mor1.Models.CompanyModels;
using mor1.Models.ReqQuoteModels;
using mor1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace mor1.Data.Repositories
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly IConfiguration _config;

        public CompanyRepo(IConfiguration config)
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

        #region Directory

        public IEnumerable<DirectoryVM> DirectoryGetAll()
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT ID, CompanyName, CompanyLogoFilename FROM Companies";
                    conn.Open();
                    var result = conn.Query<DirectoryVM>(sQuery);
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
        
        public async Task<IEnumerable<DirectoryVM>> DirGetByInitial(string initial)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT ID, CompanyName, CompanyLogoFilename FROM Companies WHERE SUBSTRING(CompanyName, 1, 1) = @Initial";
                    conn.Open();
                    var result = await conn.QueryAsync<DirectoryVM>(sQuery, new { Initial = initial });
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

        #region SearchCompany

        public async Task<IEnumerable<Company>> GetCompanyByInput(string input)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT * FROM Companies WHERE CompanyName LIKE @Input";
                    conn.Open();
                    var result = await conn.QueryAsync<Company>(sQuery, new { Input = "%" + input + "%" });
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

        public async Task<IEnumerable<SearchAutocompVM>> GetSearchAutocompByInput(string input)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT DISTINCT Comp.CompanyName, ServiceCategories.Name " +
                                    "FROM (SELECT ID, CompanyName FROM Companies WHERE CompanyName LIKE @input) AS Comp " +
                                    "INNER JOIN Companies_Services ON Comp.ID = Companies_Services.CompanyID " +
                                    "INNER JOIN Services ON Companies_Services.ServiceID = Services.ID " +
                                    "INNER JOIN ServiceCategories ON Services.CatID = ServiceCategories.ID " +
                                    "ORDER BY ServiceCategories.Name";
                    conn.Open();
                    var result = await conn.QueryAsync<SearchAutocompVM>(sQuery, new { Input = "%" + input + "%" });
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

        public async Task<IEnumerable<Company>> GetSearchResults(string input, int[] servCatIn, int[] servIn)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    var sQuery = "SELECT DISTINCT c.* FROM Companies AS c INNER JOIN Companies_Services AS cs ON c.CompanyName LIKE @Input AND c.ID = cs.CompanyID INNER JOIN Services AS s ON cs.ServiceID = s.ID";
                    var sQuery2 = "SELECT cs.CompanyID, cs.ServiceID, s.Title FROM Companies AS comp  INNER JOIN Companies_Services AS cs ON CompanyName LIKE @Input AND comp.ID = cs.CompanyID INNER JOIN Services AS s ON cs.ServiceID = s.ID;";
                    SqlMapper.GridReader results;
                    conn.Open();
                    if (servIn.Length < 1)
                    {
                        sQuery += ";";
                        results = await conn.QueryMultipleAsync(sQuery + sQuery2, new { Input = "%" + input + "%" });
                    }
                    else
                    {
                        sQuery += " AND s.ID IN @ServIn;";
                        results = await conn.QueryMultipleAsync(sQuery + sQuery2, new { Input = "%" + input + "%", ServIn = servIn });
                    }
                    var comp = results.Read<Company>().ToDictionary(k => k.ID,v=>v);
                    var compServ = results.Read<CompServ>();


                    foreach(var el in compServ)
                    {
                        //var one = comp.ContainsKey(el.CompanyID);
                        if (comp.ContainsKey(el.CompanyID))
                        {
                            if (comp[el.CompanyID].CompServList == null)
                            {
                                comp[el.CompanyID].CompServList = new List<CompServ>();
                            }
                            comp[el.CompanyID].CompServList.Add(el);
                        }
                    }

                    return comp.Values.ToList();
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

        #region IndividualCompany

        public async Task<Company> GetCompanyById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT * FROM Companies WHERE ID = @Id;";
                    string sQuery2 = "SELECT cs.CompanyID, cs.ServiceID, s.Title FROM Companies AS comp  INNER JOIN Companies_Services AS cs ON comp.ID = @Id AND comp.ID = cs.CompanyID INNER JOIN Services AS s ON cs.ServiceID = s.ID;";
                    conn.Open();
                    var result = await conn.QueryMultipleAsync(sQuery + sQuery2, new { Id = id });

                    var comp = result.Read<Company>().First();
                    var compServ = result.Read<CompServ>();

                    foreach (var el in compServ)
                    {
                        if (comp.CompServList == null)
                        {
                            comp.CompServList = new List<CompServ>();
                        }
                        comp.CompServList.Add(el);
                    }

                    return comp;
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

        public async Task<Company> GetCompanyWcommentsById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT * FROM Companies WHERE ID = @Id;";
                    string sQuery2 = "SELECT cs.CompanyID, cs.ServiceID, s.Title FROM Companies AS comp  INNER JOIN Companies_Services AS cs ON comp.ID = @Id AND comp.ID = cs.CompanyID INNER JOIN Services AS s ON cs.ServiceID = s.ID;";
                    string sQuery3 = "SELECT cm.ID, cm.CommentContent, cm.PersonName, cm.Rating, cm.CompID FROM Companies AS comp  INNER JOIN Comments AS cm ON comp.ID = @Id AND comp.ID = cm.CompID;";
                    string sQuery4 = "SELECT ph.ID, ph.FileName, ph.CompID FROM Companies AS comp  INNER JOIN ProjectPhotos AS ph ON comp.ID = @Id AND comp.ID = ph.CompID;";
                    conn.Open();
                    var result = await conn.QueryMultipleAsync(sQuery + sQuery2 + sQuery3 + sQuery4, new { Id = id });

                    var comp = result.Read<Company>().First();
                    var compServ = result.Read<CompServ>();
                    var compComments = result.Read<CompComment>();
                    var projectPhotos = result.Read<ProjectPhotos>();

                    foreach (var el in compServ)
                    {
                        if (comp.CompServList == null)
                        {
                            comp.CompServList = new List<CompServ>();
                        }
                        comp.CompServList.Add(el);
                    }

                    foreach(var el in compComments)
                    {
                        if(comp.CommentList == null)
                        {
                            comp.CommentList = new List<CompComment>();
                        }
                        comp.CommentList.Add(el);
                    }

                    foreach (var el in projectPhotos)
                    {
                        if (comp.ProjectPhotosList == null)
                        {
                            comp.ProjectPhotosList = new List<ProjectPhotos>();
                        }
                        comp.ProjectPhotosList.Add(el);
                    }

                    return comp;
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

        public async Task<bool> UpdateCompanyRating(int id)
        {
            var avgRating = (await GetAllCommentRatingByComp(id)).Average();
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "UPDATE Companies SET OverallRating = @Rating WHERE ID = @Id";
                    conn.Open();
                    //need await ??
                    var result = await conn.ExecuteAsync(sQuery, new { Rating = avgRating, Id = id });

                    return result > 0  ? true : false;
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

        #region Comments

        public async Task<int> AddComment(CompComment input)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "INSERT INTO Comments (CommentContent, PersonName, CompID, Rating) VALUES (@CommentContent, @PersonName, @CompID, @Rating);";
                        //+ "SELECT CAST(SCOPE_IDENTITY() as int);";
                    conn.Open();
                    var returnId = await conn.ExecuteAsync(sQuery, new { CommentContent = input.CommentContent, PersonName = input.PersonName, CompID = input.CompID, Rating = input.Rating });
                    return returnId;
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

        private async Task<IEnumerable<int>> GetAllCommentRatingByComp(int id)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sQuery = "SELECT cm.Rating FROM Companies AS comp  INNER JOIN Comments AS cm ON comp.ID = @Id AND comp.ID = cm.CompID;";
                    conn.Open();
                    var results = await conn.QueryAsync<int>(sQuery, new { Id = id });
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
