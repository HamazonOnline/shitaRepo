using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using DTO;

namespace BLL
{
    public class MapperGlobal
    {
        public static IMapper mapper;
        static MapperGlobal()
        {
            var config = new MapperConfiguration(cfg =>
            {
               // cfg.CreateMap<product, ProductDTO>();
               
            });
            mapper = config.CreateMapper();
        }
    }
}
