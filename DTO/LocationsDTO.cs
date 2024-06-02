using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LocationsDTO
    {
    }

    public class ColumnDTO
    {
        public int Id { get; set; }
        //set function set LocationName as int.Parse(name)

        private string name;
       public string Name
        {
            get { return name; }
            set { name = value; LocationName = int.Parse(value); }
        }

        public int LocationName { get; set; }
    }

    public class ColumnRealTimeDTO : ColumnDTO
    {
        public int Locked { get; set; }

    }

    public class RowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColumnId { get; set; }
    }

    public class RowWithLinesDTO : RowDTO
    {
        public List<LineDTO> Lines { get; set; }
    }

    public class LineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProductId { get; set; }
        //public string Barcode { get; set; }
        public Nullable<int> MaxQuantity { get; set; }
        public string ProductName { get; set; }
    }
}
