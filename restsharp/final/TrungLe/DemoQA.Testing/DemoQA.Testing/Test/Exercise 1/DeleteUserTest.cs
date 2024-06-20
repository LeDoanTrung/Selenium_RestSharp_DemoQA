using FluentAssertions;
using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Services;


namespace DemoQA.Testing.Test.Exersize1
{
    [TestFixture, Category("DeleteUser")]
    public class DeleteUserTest : BaseTest
    {
        private UserService _userService;

        public DeleteUserTest()
        {
            _userService = new UserService(ApiClient);
        }

        [Test]
        [TestCase("createdUser")]
        public async Task DeleteUser(string accountKey)
        {
            CreateUserRequestDTO user = AccountData[accountKey];

            ReportLog.Info("1. Create User");
            var createdResponse = await _userService.CreateUser(user);
            var createdResult = createdResponse.Data;
            string userId = createdResult.Data.Id;

            ReportLog.Info("2. Delete User by ID");
            var deleteUserResponse = await _userService.DeleteUser(userId);

            ReportLog.Info("3. Verify status code");
            deleteUserResponse.VerifyStatusCodeOK();
            deleteUserResponse.Data.Code.Should().Be(204);


        }
    }
}
