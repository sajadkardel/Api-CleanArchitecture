namespace Domain.Constants;

public class ConfigurationConst
{
    public JwtConfigurationConst JwtConfigurationConst { get; set; } = new();
    public IdentityConfigurationConst IdentityConfigurationConst { get; set; } = new();
}

public class JwtConfigurationConst
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public string EncryptKey { get; set; }
    public int NotBeforeMinutes { get; set; }
    public int ExpirationMinutes { get; set; }
}

public class IdentityConfigurationConst
{
    public int PasswordRequiredLength { get; set; }
    public int PasswordRequiredUniqueChars { get; set; }
    public bool PasswordRequireNonAlphanumeric { get; set; }
    public bool PasswordRequireLowercase { get; set; }
    public bool PasswordRequireUppercase { get; set; }
    public bool PasswordRequireDigit { get; set; }
    public bool RequireUniqueEmail { get; set; }
}