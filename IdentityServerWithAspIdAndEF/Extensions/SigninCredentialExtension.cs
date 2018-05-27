using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF.Extensions
{
    /// <summary>
    /// Impl of adding a signin key for identity server 4,
    /// with an appsetting.json configuration look similar to:
    /// "SigninKeyCredentials": {
    ///     "KeyType": "KeyFile",
    ///     "KeyFilePath": "C:\\certificates\\idsv4.pfx",
    ///     "KeyStorePath": ""
    /// }
    /// </summary>
    public static class SigninCredentialExtension
    {
        private const string KeyType = "KeyType";
        private const string KeyTypeKeyFile = "KeyFile";
        private const string KeyTypeKeyStore = "KeyStore";
        private const string KeyTypeTemporary = "Temporary";
        private const string KeyFilePath = "KeyFilePath";
        private const string KeyFilePassword = "KeyFilePassword";
        private const string KeyStoreIssuer = "KeyStoreIssuer";
        public static string KeyPersistPath = "KeyPersistPath";


        public static IIdentityServerBuilder AddSigninCredentialFromConfig(
            this IIdentityServerBuilder builder, IConfigurationSection options)
        {
            string keyType = options.GetValue<string>(KeyType);

            switch (keyType)
            {

                case KeyTypeKeyFile:
                    AddCertificateFromFile(builder, options);
                    break;

                case KeyTypeKeyStore:
                    AddCertificateFromStore(builder, options);
                    break;
            }

            return builder;
        }

        private static void AddCertificateFromStore(IIdentityServerBuilder builder,
            IConfigurationSection options)
        {
            var keyIssuer = options.GetValue<string>(KeyStoreIssuer);

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);

            if (certificates.Count > 0)
                builder.AddSigningCredential(certificates[0]);
        }

        private static void AddCertificateFromFile(IIdentityServerBuilder builder,
            IConfigurationSection options)
        {
            var keyFilePath = options.GetValue<string>(KeyFilePath);
            var keyFilePassword = options.GetValue<string>(KeyFilePassword);

            if (File.Exists(keyFilePath))
            {
                try
                {
                    X509Certificate2 cert = new X509Certificate2(keyFilePath, keyFilePassword,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                    builder.AddSigningCredential(cert);
                }
                catch (Exception ex)
                {
                    throw new Exception("CertificateFromFile   found  but error occurred while adding certificate:" + keyFilePath,ex);
                }
               
            }
            else
            {
                throw new Exception("CertificateFromFile not found :" + keyFilePath);
            }
        }
    }
}
