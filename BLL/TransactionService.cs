using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public  class TransactionService
    {
        public static TransactionService Instance;
        public static TransactionService instance
        {
            get
            {
                if (Instance == null)
                    Instance = new TransactionService();
                return Instance;
            }
        }
        private TransactionService() { }

        public List<TransactionDTO> GetTransactionRealTime(int _locationId)
        {
            using(shitaEntities context = new shitaEntities())
            {
                var v = from t in context.product_transactions
                        join p in context.products on t.product_id equals p.id
                        join c in context.carts on t.cart_id equals c.id
                      join l in context.locations on c.location_id equals l.id
                        //join col in context.columns on
                        join u in context.users on c.user_id equals u.id
                        where l.id == _locationId
                        select (new TransactionDTO()
                        {
                            Id = t.id,
                            Barcode = p.barcode,
                            ProductName = p.name,
                            CartId = c.id,
                            //ColumnId = pl.column_id,
                            //ColumnName = pl.column_id,
                            Timestamp = (DateTime)t.insert_date,
                            Quantity = (int)t.quantity,
                            UserName = u.fullname
                        }) ;
                return v.ToList();
            }
        }

    }
}
