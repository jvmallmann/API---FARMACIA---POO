using AutoMapper;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;

namespace AVALIAÇÃO_A4.Mappings
{
    //  configuração para o AutoMapper
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // configuração do mapeamento entre as entidades Sale e SaleDTO
            CreateMap<Sale, SaleDTO>().ReverseMap();

            // configuração do mapeamento entre as entidades Customer e CustomerDTO
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            // configuração do mapeamento entre as entidades Remedy e RemedyDTO
            CreateMap<Remedy, RemedyDTO>().ReverseMap();

            // configuração do mapeamento entre as entidades Recipe e RecipeDTO
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
        }
    }
}
