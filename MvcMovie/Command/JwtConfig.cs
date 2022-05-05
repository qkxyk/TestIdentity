namespace MvcMovie.Command
{
    public class JwtConfigOptions
    {
        public const string Position = "JwtSetting";
        public string Issuer { get; set; } = "hx";
        public string Audience { get; set; } = "hx";
        public string IssuerSigningKey { get; set; } = "hxhjapicloud2020";
        public double AccessTokenExpiresMinutes { get; set; }
    }

    public class CookieConfigOptions
    {
        public const string Position = "CookieSettings";
        public string ExpireTimeSpan { get; set; }
        public string LoginPath { get; set; }
        public string LogoutPath { get; set; }
    }
}
