namespace Pfts.Infrastucture.Extensions.DataSeeding;

using System;
using Ravm.Infrastructure.Persistence.EntityFramework;

public static class SeedDataExtensions
{
    public static void SeedAsync(this AppDbContext context, IMapper mapper)
    {
        var projectDirectory = AppContext.BaseDirectory;



        if (string.IsNullOrWhiteSpace(projectDirectory))
        {
            throw new DirectoryNotFoundException($"Root directory not found");
        }

        if (!context.Organizations.Any())
            AddInitialOrganization(context);

        if (!context.Employees.Any())
            AddInitialEmployee(context, mapper!);

        if (!context.Countries.Any())
        {

            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("Countries.xlsx");

                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "Countries");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadCountriesFromExcel();
                    if (items.Count > 0)
                    {
                        context.Countries.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedCountries");
            }
        }

        if (!context.Regions.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("Regions.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "Regions");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadRegionsFromExcel(context);
                    if (items.Count > 0)
                    {
                        context.Regions.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedRegions");
            }
        }

        if (!context.Cities.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("Cities.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "Cities");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadCitiesFromExcel(context);
                    if (items.Count > 0)
                    {
                        context.Cities.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedCities");
            }
        }

        if (!context.Occupations.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("Occupations.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "Occupations");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadOccupationsFromExcel();
                    if (items.Count > 0)
                    {
                        context.Occupations.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedOccupations");
            }
        }

        if (!context.Specializations.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("Specializations.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "Specializations");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadSpecializationsFromExcel();
                    if (items.Count > 0)
                    {
                        context.Specializations.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedSpecializations");
            }
        }

        if (!context.VehicleMarks.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("VehicleMarks.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "VehicleMarks");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadVehicleMarksFromExcel();
                    if (items.Count > 0)
                    {
                        context.VehicleMarks.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedVehicleMarks");
            }
        }

        if (!context.VehicleModels.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("VehicleModels.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "VehicleModels");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadVehicleModelsFromExcel(context);
                    if (items.Count > 0)
                    {
                        context.VehicleModels.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedVehicleModels");
            }
        }

        if (!context.Reasons.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("Reasons.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "Reasons");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadReasonsFromExcel();
                    if (items.Count > 0)
                    {
                        context.Reasons.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedReasons");
            }
        }

        if (!context.RouteClassifications.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("RouteClassifications.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "RouteClassifications");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadRouteClassificationsFromExcel();
                    if (items.Count > 0)
                    {
                        context.RouteClassifications.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedRouteClassifications");
            }
        }

        if (!context.StopPoints.Any())
        {
            try
            {
                var excelFilePath = projectDirectory.CombineWithProjectDirectory("StopPoints.xlsx");
                var dataTable = ExcelFileUtils.GetDataFromExcel(excelFilePath, "StopPoints");

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    var items = dataTable.ReadStopPointsFromExcel();
                    if (items.Count > 0)
                    {
                        context.StopPoints.AddRange(items);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message, "Error on SeedStopPoints");
            }
        }
    }

    private static void AddInitialOrganization(AppDbContext? context)
    {
        var organization = new Organization()
        {
            Id = Guid.Parse("6B34439F-4FAE-450E-93A2-16A280C2BF31"),
            Code = "admin",
            Tin = "admin",
            Name = "Admin",
            NameRu = "AdminRu",
            NameKa = "AdminKa",
            NameUz = "AdminUz",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            CreatedById = Guid.Parse("60CE452C-6778-47EC-BA07-C745E7BA6C04"),
            UpdatedById = Guid.Parse("60CE452C-6778-47EC-BA07-C745E7BA6C04")
        };

        context!.Add(organization);
        context.SaveChanges();
    }

    private static void AddInitialEmployee(AppDbContext? context, IMapper mapper)
    {
        var user = context!.Users.FirstOrDefault();

        var employeeCommand = new UpdateEmployeeCommand(
            "admin",
            "admin",
            "admin",
            Gender.Male,
            DateTimeOffset.UtcNow.AddYears(-20),
            Guid.Parse("6B34439F-4FAE-450E-93A2-16A280C2BF31"),
            OccupationGroupType.Admin,
            "",
            "12345",
            "12345678910121");

        employeeCommand!.Id = Guid.Parse("745F5930-49A7-4191-B64D-65719AFD7239");

        var employee = mapper.Map<Employee>(employeeCommand);

        employee.UserId = Guid.Parse("60CE452C-6778-47EC-BA07-C745E7BA6C04");

        context!.Add(employee);
        context.SaveChanges();
    }

}

