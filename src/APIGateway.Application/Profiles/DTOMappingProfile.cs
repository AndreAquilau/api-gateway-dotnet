using APIGateway.Domain.CEP.ObjectValues;
using APIGateway.Application.Presenters.CEP;
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
        CreateMap<CEPObjectValue, CEPPresenter>().ReverseMap();
    }
}
