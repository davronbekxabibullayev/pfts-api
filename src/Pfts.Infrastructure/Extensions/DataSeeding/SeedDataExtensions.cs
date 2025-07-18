/*namespace Pfts.Infrastructure.Extensions.DataSeeding;

using System;
using AutoMapper;
using Pfts.Infrastructure.Persistance.EntityFramework;

public static class SeedDataExtensions
{
    public static void SeedAsync(this AppDbContext context, IMapper mapper)
    {
        var projectDirectory = AppContext.BaseDirectory;



        if (string.IsNullOrWhiteSpace(projectDirectory))
        {
            throw new DirectoryNotFoundException($"Root directory not found");
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
}

*/
