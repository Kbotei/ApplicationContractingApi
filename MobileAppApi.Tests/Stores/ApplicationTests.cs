using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;
using MobileAppApi.Stores;
using Moq;
using Moq.EntityFrameworkCore;

namespace MobileAppApi.Tests.Stores
{
    [TestClass()]
    public class ApplicationTests
    {
        // TODO: Fix transaction mocking issue.
        [TestMethod()]
        public void TestApplicationStoreSaveData()
        {
            var loggerMock = new Mock<ILogger<ApplicationStore>>();

            var applicationSubmission = new ApplicationSubmissionRequest {
                SubmissionId = Guid.NewGuid(),
                Fields = new List<OrderedField>()
            };

            var applicationMock = new List<ApplicationSubmission>()
            {
                new ApplicationSubmission
                {
                    Id = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    DeviceId = Guid.NewGuid(),
                    SubmissionId = Guid.NewGuid(),
                    SubmittedAt = DateTime.Now,
                    ApplicationFieldSubmissions = new List<ApplicationFieldSubmission>()
                }
            }.AsQueryable();

            var dbContextMock = new Mock<MobileApiContext>();
            var mockTransaction = new Mock<IDbContextTransaction>();
            var mockDatabase = new Mock<DatabaseFacade>(dbContextMock.Object);

            mockDatabase.Setup(d => d.BeginTransaction()).Returns(mockTransaction.Object);
            mockDatabase.Setup(d => d.CommitTransaction()).Verifiable();
            mockDatabase.Setup(d => d.RollbackTransaction()).Verifiable();

            dbContextMock.Setup(c => c.Database).Returns(mockDatabase.Object);
            dbContextMock.Setup(x => x.ApplicationSubmissions).ReturnsDbSet(applicationMock);

            var applicationStore = new ApplicationStore(loggerMock.Object, dbContextMock.Object);
            var result = applicationStore.SaveApplicationData(applicationSubmission);

            Assert.IsTrue(result.Result);
        }

        [TestMethod()]
        public void TestDuplicateApplicationStoreSaveData()
        {
            var loggerMock = new Mock<ILogger<ApplicationStore>>();
            var submissionId = Guid.NewGuid();

            var applicationSubmission = new ApplicationSubmissionRequest
            {
                SubmissionId = submissionId,
                Fields = new List<OrderedField>()
            };

            var applicationMock = new List<ApplicationSubmission>()
            {
                new ApplicationSubmission
                {
                    Id = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    DeviceId = Guid.NewGuid(),
                    SubmissionId = submissionId,
                    SubmittedAt = DateTime.Now,
                    ApplicationFieldSubmissions = new List<ApplicationFieldSubmission>()
                }
            }.AsQueryable();
            var dbContextMock = new Mock<MobileApiContext>();
            dbContextMock.Setup(x => x.ApplicationSubmissions).ReturnsDbSet(applicationMock);

            var applicationStore = new ApplicationStore(loggerMock.Object, dbContextMock.Object);
            var result = applicationStore.SaveApplicationData(applicationSubmission);

            Assert.IsTrue(result.Result);
        }
    }
}