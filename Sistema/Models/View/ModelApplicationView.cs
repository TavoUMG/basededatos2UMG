namespace Sistema.Models.View
{
    public class ModelApplicationView
    {
        public string nameAPP { get; internal set; } = Environment.GetEnvironmentVariable("APP_NAME");
        public string version { get; internal set; } = Environment.GetEnvironmentVariable("version");
        public string versionInstalada { get; internal set; } = Environment.GetEnvironmentVariable("versionInstalada");
        public string versionRegistrada { get; internal set; } = Environment.GetEnvironmentVariable("versionRegistrada");
        public string versionEnvironment { get; internal set; } = Environment.Version.ToString();
        public string osversion { get; internal set; } = Environment.OSVersion.ToString();
        public string userName { get; internal set; } = Environment.UserName.ToString();
        public string usuarioInteractive { get; internal set; } = Environment.UserInteractive.ToString();
        public string? actionController { get; internal set; }
    }
}
