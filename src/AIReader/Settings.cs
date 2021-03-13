using System;
using System.IO;
using Microsoft.Extensions.Configuration;

static class Settings
{
    static readonly IConfiguration _instance = 
        new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(typeof(Program).Assembly.Location))
            .AddJsonFile("appsettings.json")
            .Build();

    public static ushort HttpTimeout => _instance.GetValue<ushort>("httpTimeout"); 
    public static Guid AIApplicationId => _instance.GetValue<Guid>("aiApplicationId"); 
    public static string AIApiKey => _instance.GetValue<string>("aiApiKey"); 
    public static string TimesAgo => _instance.GetValue<string>("timesAgo"); 
}