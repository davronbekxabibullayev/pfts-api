namespace Pfts.Infrastucture.Extensions.DataSeeding;

using System.Data;

public static class DataSeeder
{
    public static List<City> ReadCitiesFromExcel(this DataTable dataTable, IAppDbContext dbContext)
    {
        var result = new List<City>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cells = row.ItemArray;
            var regionCode = cells[0]!.ToString()!.Trim();
            if (!string.IsNullOrEmpty(regionCode))
            {
                var region = dbContext.Regions.FirstOrDefault(c =>
                c.Code == regionCode.ToString()!);
                result.Add(new City()
                {
                    RegionId = region!.Id,
                    Code = cells[1]!.ToString()!,
                    StateCode = cells[2]?.ToString(),
                    Name = cells[3]?.ToString()!,
                    NameRu = cells[4]?.ToString()!,
                    NameUz = cells[5]?.ToString(),
                    NameKa = cells[6]?.ToString(),
                });
            }
        }
        return result;
    }
}
