namespace Ravm.Api.Configuration;

public class LocalConfiguration
{
    public static string WebRootPath { get; set; } = default!;
    public static string ContentRootPath { get; set; } = default!;
    public static string SeedDataContent => Environment.OSVersion.Platform == PlatformID.Win32NT ? "App_Data\\SeedData" : "App_Data/SeedData";
    public static string Templates => Environment.OSVersion.Platform == PlatformID.Win32NT ? "App_Data\\Templates" : "App_Data/Templates";
    public static string Certificates => Environment.OSVersion.Platform == PlatformID.Win32NT ? "App_Data\\Certificates" : "App_Data/Certificates";
    public static string Images => Environment.OSVersion.Platform == PlatformID.Win32NT ? "App_Data\\Images" : "App_Data/Images";
    public static string QrCodes => Environment.OSVersion.Platform == PlatformID.Win32NT ? "App_Data\\QrCodes" : "App_Data/QrCodes";
    public static bool IsDevelopment { get; set; }
}

