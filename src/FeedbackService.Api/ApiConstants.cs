using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackService.Api
{
    public class ApiConstants
    {
        public const string ServiceName = "feedbackservice";
        public const string FriendlyServiceName = "Feedback Service";
    }
    public class KeyVaultKeys
    {
        public const string FeedbackDbConnectionString = "feedback-api-db-connectionstring";
        public const string StorageAccountSecret = "storage-account-secret";
    }
}
