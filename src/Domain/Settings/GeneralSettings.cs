namespace Domain.Settings
{
    public class GeneralSettings
    {
        public string ElmahPath { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }
    }
}
