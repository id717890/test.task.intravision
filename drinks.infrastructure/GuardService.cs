namespace drinks.infrastructure
{
    using drinks.infrastructure.@interface;
    using System.Configuration;

    public class GuardService
    {
        public static bool CheckSecret(string secret)
        {
            if (secret == null) return false;
            var configSecret = ConfigurationManager.AppSettings["secret"];
            return configSecret.ToLower() == secret.ToLower();
        }
    }
}
