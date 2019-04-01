using mor1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mor1.Data.IRepositories
{
    public interface ICountryRepo
    {
        IEnumerable<Country> GetAll();
    }
}
