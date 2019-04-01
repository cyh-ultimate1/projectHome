using mor1.Models.CompanyModels;
using mor1.Models.ReqQuoteModels;
using mor1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.IRepositories
{
    public interface ICompanyRepo
    {
        IEnumerable<DirectoryVM> DirectoryGetAll();
        Task<IEnumerable<DirectoryVM>> DirGetByInitial(string initial);
        Task<IEnumerable<Company>> GetCompanyByInput(string input);
        Task<IEnumerable<SearchAutocompVM>> GetSearchAutocompByInput(string input);
        Task<IEnumerable<Company>> GetSearchResults(string input, int[] servCatIn, int[] servIn);
        Task<Company> GetCompanyById(int id);
        Task<Company> GetCompanyWcommentsById(int id);
        Task<int> AddComment(CompComment input);
        Task<bool> UpdateCompanyRating(int id);
        //Task<IEnumerable<int>> GetAllCommentRatingByComp(int id);
    }
}
