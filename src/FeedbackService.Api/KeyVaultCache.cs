using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackService.Api
{
	public static class KeyVaultCache
	{
		public static string BaseUri { get; set; }

		private static KeyVaultClient _KeyVaultClient = null;
		public static KeyVaultClient KeyVaultClient
		{
			get
			{
				if (_KeyVaultClient is null)
				{
					var provider = new AzureServiceTokenProvider();
					_KeyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(provider.KeyVaultTokenCallback));
				}
				return _KeyVaultClient;
			}
		}

		public static void GetAzureKeyVaultSecrets(HostBuilderContext context, IConfigurationBuilder config)
		{
			//var tenantID = "29f32d28-5f20-4426-8f9b-fadfe5fd0ddb";
			//var appID = "1f1c90f4-6d79-4004-aeb2-f930c2c2136e";
			//var appSec = "Jv27Q~hbcq2Z6k.pEeFdWjQ9.7BDtaTICOqvm";

			var builtConfig = config.Build();
			var keyVaultName = builtConfig[$"AppSettings:KeyVaultName"];
			BaseUri = $"https://{keyVaultName}.vault.azure.net";
			var secretClient = new SecretClient(
							new Uri(BaseUri),
							new DefaultAzureCredential());
							//new ClientSecretCredential(tenantID, appID, appSec));
			config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
		}
		private static Dictionary<string, string> SecretsCache = new Dictionary<string, string>();
		public async static Task<string> GetCachedSecret(string secretName)
		{
			if (!SecretsCache.ContainsKey(secretName))
			{
				var secretBundle = await KeyVaultClient.GetSecretAsync($"{BaseUri}/secrets/{secretName}").ConfigureAwait(false);
				SecretsCache.Add(secretName, secretBundle.Value);
			}
			return SecretsCache.ContainsKey(secretName) ? SecretsCache[secretName] : string.Empty;
		}
	}
	public static class GetSecret
	{
		public static async Task<string> FeedbackDbConnectionString() => await KeyVaultCache.GetCachedSecret($"{KeyVaultKeys.FeedbackDbConnectionString }");
		public static async Task<string> StorageAccountSecret() => await KeyVaultCache.GetCachedSecret($"{KeyVaultKeys.StorageAccountSecret }");
	}
}
