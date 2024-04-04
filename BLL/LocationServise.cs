using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LocationServise
    {
        public static LocationServise Instance;
        public static LocationServise instance
        {
            get
            {
                if (Instance == null)
                    Instance = new LocationServise();
                return Instance;
            }
        }
        private LocationServise() { }

        public LineDTO AddLine(int rowId)
        {
            using(shitaEntities context = new shitaEntities())
            {
                lines l = new lines();
                l.row_id = rowId;
                //line_name is highest line name in the row + 1
                int lineName = int.Parse((from line in context.lines where line.row_id == rowId select line.line_name).Max());
                l.line_name = (lineName + 1).ToString(); 
                context.lines.Add(l);
                context.SaveChanges();
                return new LineDTO()
                {
                    Id = l.id,
                    Name = l.line_name,
                };
            }
        }

        public bool RemoveLine(int lineId)
        {
            using(shitaEntities context = new shitaEntities())
            {
                lines l = context.lines.FirstOrDefault(x => x.id == lineId);
                if (l == null)
                    return false;
                context.lines.Remove(l);
                context.SaveChanges();
                return true;
            }
        }

        //function get userId, password and locationId and return userId and locationId
        public SimpleDTO GetUserId (string userName, string password)
        {
            using(shitaEntities context = new shitaEntities())
            {
                var v = from ul in context.users
                        where ul.username == userName && ul.password == password
                        select (new SimpleDTO { Id = ul.id, Name = ul.fullname});
                return v.FirstOrDefault();
            }
        }

        public List<SimpleDTO> GetAllLocations()
        {
            //return list of locations as simpleDto
            using (shitaEntities context = new shitaEntities())
            {
                var v = from l in context.locations
                        select new SimpleDTO()
                        {
                            Id = l.id,
                            Name = l.name
                        };
                return v.ToList();
            }
        }

        public List<ColumnDTO> GetColumnsPerLocation(int locationId)
        {
            using(shitaEntities context = new shitaEntities())
            {
                var v = from c in context.columns
                        where c.location_id == locationId
                        select new ColumnDTO()
                        {
                            Id = c.id,
                            Name = c.column_name,
                            //LocationName = int.Parse(c.location_id.ToString())
                        };
                return v.ToList();
            }
        }

        public List<RowWithLinesDTO> GetRowsPerColumn(int columnId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from r in context.rows
                        //left join to product
                        ///join l in context.lines on r.id equals l.row_id
                        where r.column_id == columnId

                        select new RowWithLinesDTO()
                        {
                            Id = r.id,
                            Name = r.row_name,
                            Lines = (from l in context.lines
                                     join lp in context.product_locations on l.id equals lp.line_id into p
                                     from pl in p.DefaultIfEmpty()
                                     join pr in context.products on pl.product_id equals pr.id into pro from prod in pro.DefaultIfEmpty()
                                     where l.row_id == r.id
                                     orderby l.line_name
                                     select new LineDTO()
                                     {
                                         Id = l.id,
                                         Name = l.line_name,
                                         ProductId = pl.product_id,
                                         MaxQuantity = pl.product_max_quantity,
                                         ProductName = prod.name
                                     }).ToList()
                        };
                return v.ToList();
            }
        }

        public void SetProductInLine(int id, int lineId)
        {
            using(shitaEntities context = new shitaEntities())
            {
                product_locations pl =  context.product_locations.Where(x=> x.line_id == lineId).FirstOrDefault();
                if (pl != null)
                {
                    pl.product_id = id;
                    context.SaveChanges();
                }
                else
                {
                    pl = new product_locations();
                    pl.product_id = id;
                    pl.line_id = lineId;
                    context.product_locations.Add(pl);
                    context.SaveChanges();

                }
            }
        }
    }
}
