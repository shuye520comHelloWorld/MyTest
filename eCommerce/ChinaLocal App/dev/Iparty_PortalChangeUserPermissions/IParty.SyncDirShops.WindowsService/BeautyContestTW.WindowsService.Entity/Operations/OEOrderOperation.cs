using BeautyContestTW.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyContestTW.WindowsService.Entity.Operations
{
    public class OEOrderOperation : BaseEntityOperation<Order>
    {
        private const string uspOrderDataGetForMyCustomer = "Basket.dbo.uspOrderDataGetForMyCustomer";
        private const string uspOrderDataGetForMyCustomertest = "Basket.dbo.uspOrderDataGetForMyCustomerTest";
        public OEOrderOperation(string dbStr) : base(dbStr) { }

        public DataSet RetrieveOrderDetail(Guid OrderId)
        {
            return RepositoryHelper.ExecuteDataset
                (ConnectionStrings.Basket, uspOrderDataGetForMyCustomer, new object[] { OrderId });
            
        }

        private Func<DataSet> OrderDetail()
        {
            throw new NotImplementedException();
        }


    }


}
