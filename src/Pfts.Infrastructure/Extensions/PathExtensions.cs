namespace Pfts.Infrastructure.Extensions;

public static class PathExtensions
{
    public static string CombineWithProjectDirectory(this string projectDirectory, string fileName)
    {
        return Path.Combine(projectDirectory, "SeedDatas", fileName);
    }
}
