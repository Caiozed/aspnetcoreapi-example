using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AspNetCoreApiExample.Security.Configuration
{
    public class SignInConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
        public SignInConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256);
        }
    }
}
