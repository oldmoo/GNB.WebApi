using AutoMapper;
using GNB.Application.Dtos;
using GNB.Domain.Entities;

namespace GNB.Application.Helpers.Mappers;

public class MappingProfile : Profile
{
     public MappingProfile()
     {
          CreateMap<RateDto, Rate>().ReverseMap();
          CreateMap<TransactionDto, Transaction>().ReverseMap();
     }
}