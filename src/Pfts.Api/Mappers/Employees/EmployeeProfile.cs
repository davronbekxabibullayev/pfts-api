namespace Ravm.Api.Mappers.Employees;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<UpdateEmployeeRequest, UpdateEmployeeCommand>();
    }
}
