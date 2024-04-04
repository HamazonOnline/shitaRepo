using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public int ColumnId { get; set; }
        public int ColumnName { get; set; }
        public DateTime Timestamp { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        public int CartId { get; set; }



    }
}
