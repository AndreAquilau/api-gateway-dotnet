using APIGateway.Domain.CEP.ObjectValues;
using APIGateway.Application.DTOs.CEPDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Application.Profiles;
public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<CEPObjectValue, CEPDto>().ReverseMap();
    }
}
