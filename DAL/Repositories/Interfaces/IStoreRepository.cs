using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IStoresRepository
    {
        Store Add(Store entity);
        Store Get(int id);
        IEnumerable<Store> GetAll(SearchParam searchParam = null);
        int Count(SearchParam searchParam = null);
        Store Update(Store entity);
    }
}
