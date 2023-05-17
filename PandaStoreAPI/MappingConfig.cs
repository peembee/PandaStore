using AutoMapper;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using PandaStore.Models.DTO;

namespace PandaStoreAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}


