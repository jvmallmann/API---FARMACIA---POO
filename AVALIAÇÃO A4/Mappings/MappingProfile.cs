using AutoMapper;
using AVALIAÇÃO_A4.DataBase.DTO;
using AVALIAÇÃO_A4.DataBase.Models;

namespace AVALIAÇÃO_A4.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Venda, VendaDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Remedio, RemedioDTO>().ReverseMap();
            CreateMap<Receita, ReceitaDTO>().ReverseMap();
        }
    }
}
