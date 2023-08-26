using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackService.Api
{
    public class AppSettings
    {
        public string KeyVaultName { get; set; }
        public bool ByPassKeyVault { get; set; }
    }
}
