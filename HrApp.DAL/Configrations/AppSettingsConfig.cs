namespace HrApp.DAL.Configratins;

public class AppSettingsConfig
{
    public const string Name = "AppSettings";
    public string Secret { get; set; } = string.Empty;
    public int RefreshTokenTTL { get; set; } = int.MaxValue;

}