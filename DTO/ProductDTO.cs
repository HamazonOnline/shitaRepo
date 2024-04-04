using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; } // = 0
        public Nullable<double> Price { get; set; }
        public Nullable<double> Weight { get; set; }
        public int? ProviderId { get; set; }
    }

    public class ProductLocationDTO : ProductDTO
    {
        public int ProductLocationId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
      
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int RowId { get; set; }
        public string RowName { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }
    }

}