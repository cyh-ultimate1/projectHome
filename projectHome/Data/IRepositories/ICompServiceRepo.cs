using mor1.Models.ReqQuoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.IRepositories
{
    public interface ICompServiceRepo
    {
        IEnumerable<Service> GetAllServices();
        IEnumerable<Service> GetServicesListById(string[] id);
        IEnumerable<ServiceCategory> GetAllCat();
        int AddFloorPlanFile(string filename, string filepath, int quoteFormId);
        int AddQuoteForm(ReqQuoteForm quote);
        int AddSelectedService(int quoteFormId, int serviceId);
        Task<IEnumerable<ReqQuoteForm>> GetReqQuoteForms();
    }
}
