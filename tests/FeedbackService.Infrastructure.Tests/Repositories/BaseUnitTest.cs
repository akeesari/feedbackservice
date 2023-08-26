using FeedbackService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FeedbackService.Infrastructure.Tests.Repositories
{
    public class BaseUnitTest
    {
        protected readonly FeedbackDbContext dbContextMock;
        public static FeedbackDbContext GetFeedbackDbContext(string dbName)
        {
            // Create db context options specifying in memory database
            var options = new DbContextOptionsBuilder<FeedbackDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            //Use this to instantiate the db context
            return new FeedbackDbContext(options);
        }
        public BaseUnitTest()
        {
            dbContextMock = GetFeedbackDbContext("Feedback");
        }
        ~BaseUnitTest()
        {
            dbContextMock.Dispose();
        }
    }


}
