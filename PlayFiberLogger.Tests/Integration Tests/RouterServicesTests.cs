using PlayFiberLogger.Services;

namespace PlayFiberLogger.Tests.Integration_Tests
{
    public class RouterServicesTests
    {
        private readonly string cLOGIN = "";    // Login
        private readonly string cPASS = "";     // Password
        private readonly string cIP = "";       // IP Address Default Gateway

        [Fact]
        public async Task Login_ShouldUpdateCurrentSid_WhenCredentialsAreValid()
        {
            // Arrange
            var service = RouterService.Instance;
            service.Initialize(cIP);

            // Act
            bool success = await service.Authenticate(this.cLOGIN, cPASS, cIP);

            // Assert
            Assert.True(success);
            Assert.NotNull(service.CurrentSid);
            Assert.True(service.CurrentSid.Length > 10);
        }
    }
}
