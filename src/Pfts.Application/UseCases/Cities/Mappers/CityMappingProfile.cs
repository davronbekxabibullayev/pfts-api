namespace Pfts.Application.UseCases.Cities.Mappers;

using Pfts.Application.UseCases.Cities.Commands;
using Pfts.Domain.Models;
using Ravm.Application.UseCases.Cities.Models;

internal class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityModel>();
        CreateMap<UpdateCityCommand, City>();
    }
}
