using mor1.Data.Repositories;
using mor1.Models.ReqQuoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.IRepositories
{
    public interface IPropertyRepo
    {
        IEnumerable<PropertyType> GetAllPropertyTypes();
        IEnumerable<PropertyStatus> GetAllPropertyStatus();
    }
}
