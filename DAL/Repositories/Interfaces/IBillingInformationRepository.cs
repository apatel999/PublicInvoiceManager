using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IBillingInformationRepository
    {
        BillingInformation Add(BillingInformation entity);
        BillingInformation Update(BillingInformation entity);
        int Count(SearchParam searchParam);
        BillingInformation Get(int id);
        IEnumerable<BillingInformation> GetAll(SearchParam searchParam = null);
        BillingStatement GetWeeklyBillingStatement(int billingInformationId, int yearId, int week);
    }
}
