using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Text.RegularExpressions;
using AutoMapper.QueryableExtensions.Impl;

namespace BLL
{
    public class ProductService
    {

        public static ProductService Instance;
        public static ProductService instance
        {
            get
            {
                if (Instance == null)
                    Instance = new ProductService();
                return Instance;
            }
        }
        private ProductService() { }

        public void RemoveProductLocation(int id)
        {
            using (shitaEntities context = new shitaEntities())
            {
                product_locations p = context.product_locations.First(x => x.id == id);
                context.product_locations.Remove(p);
                context.SaveChanges();
            }
        }

        public List<ProductLocationDTO> GetProductPerColumn(int ColumnId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from pl in context.product_locations
                        join p in context.products on pl.product_id equals p.id
                        join lin in context.lines on pl.line_id equals lin.id

                        join r in context.rows on lin.row_id equals r.id
                        join col in context.columns on r.column_id equals col.id
                        where col.id == ColumnId
                        select (new ProductLocationDTO()
                        {
                            Id = p.id,
                            Name = p.name,
                            Price = (double?)p.price,
                            Weight = (double?)p.weight,
                            ProductLocationId = pl.id,
                            LineId = lin.id,
                            LineName = lin.line_name,
                            ColumnId = col.id,
                            ColumnName = col.column_name,
                            RowId = r.id,
                            RowName = r.row_name
                        });
                return v.ToList<ProductLocationDTO>();
            }
        }

        public List<SimpleDTO> GetLocations()
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from l in context.locations select (new SimpleDTO() { Id = l.id, Name = l.name });
                return v.ToList();
            }
        }

        public void InsertLocation(int productId, int loactionId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                context.product_locations.Add(new product_locations() { product_id = productId, line_id = loactionId });
                context.SaveChanges();
            }
        }

        public List<SimpleDTO> GetColumnsPerLocation(int locationId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from c in context.columns.Where(x=> x.location_id == locationId )select (new SimpleDTO() { Id = c.id, Name = c.column_name });
                return v.ToList();
            }
        }

        public List<SimpleDTO> GetRowsPerLocation(int columnId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from r in context.rows.Where(x => x.column_id == columnId) select (new SimpleDTO() { Id = r.id, Name = r.row_name });
                return v.ToList();
            }
        }

        public List<SimpleDTO> GetLinesPerLocation(int rowId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from l in context.lines.Where(x => x.row_id == rowId) select (new SimpleDTO() { Id = l.id, Name = l.line_name });
                return v.ToList();
            }
        }

        public int insertProduct(ProductDTO product)
        {
            using(shitaEntities context = new shitaEntities())
            {
                products p  = new products() { name = product.Name, price = (decimal?)product.Price, weight = (decimal?)product.Weight };
                context.products.Add( p);
                context.SaveChanges();
                return p.id;
            }
        }

        public List<ProductDTO> GetProductsPerLocation(int _locationId)
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from p in context.products
                        join p_l in context.product_locations on p.id equals p_l.product_id
                        where p_l.location_id == _locationId
                        //grooup by product id
                        group p by p.id into g
                        select (new ProductDTO()
                        {
                            Id = g.FirstOrDefault().id,
                            Barcode = g.FirstOrDefault().barcode,
                            Name = g.FirstOrDefault().name,
                            Price = (double?)g.FirstOrDefault().price,
                            Weight = (double?)g.FirstOrDefault().weight
                        });

                        //select (new ProductDTO()
                        //{
                        //    Id = p.id,
                        //    Barcode = p.barcode,
                        //    Name = p.name,
                        //    Weight = (double?)p.weight
                        //});
                return v.ToList();
            }
        }

        public List<ProductLocationDTO> GetProductsWithLocation()
        {
            using (shitaEntities context = new shitaEntities())
            {
                var v = from pl in context.product_locations
                        join p in context.products on pl.product_id equals p.id
                        join lin in context.lines on pl.line_id equals lin.id
                        
                        join r in context.rows on lin.row_id equals r.id
                        join col in context.columns on r.column_id equals col.id
                        join loc in context.locations on col.location_id equals loc.id
                        
                        select (new ProductLocationDTO()
                        {
                            Id = p.id,
                            Name = p.name,
                            Price = (double?)p.price,
                            Weight = (double?)p.weight,
                            ProductLocationId = pl.id,
                            LocationId = loc.id,
                            LocationName = loc.name,
                            LineId = lin.id,
                            LineName= lin.line_name,
                            ColumnId = col.id,
                            ColumnName = col.column_name,
                            RowId = r.id,
                            RowName = r.row_name
                        });
                return v.ToList<ProductLocationDTO>();
            }
        }

        public List<ProductDTO> GetProductsBySearch(string barcode, string name)
        {
           //find product with same barcode or end with this barcoe or similiar name
           using(shitaEntities context = new shitaEntities())
            {
                var v = from p in context.products
                        where  ( p.barcode.EndsWith(barcode) &&
                         p.name.Contains(name))
                        select (new ProductDTO()
                        {
                            Id= p.id,
                            Barcode = p.barcode,
                            Name = p.name,
                            //Price = (double?)p.price,
                            //Weight = (double?)p.weight
                        });
                return v.ToList();
            }
        }

        public void UpdateProductPrice(int id, double? price)
        {
            using (shitaEntities context = new shitaEntities())
            {
                products p = context.products.First(x => x.id == id);
                p.price = (decimal?)price;
                context.SaveChanges();
            }
        }
        //public List<SimpleDTO> GetBrands()
        //{
        //    using (sharey_revahaEntities context = new sharey_revahaEntities())
        //    {
        //        return (MapperGlobal.mapper.Map<List<SimpleDTO>>(context.p_brand).ToList());
        //    }
        //}
    }
}
