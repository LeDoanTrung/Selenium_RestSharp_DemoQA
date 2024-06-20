using FluentAssertions;
using DemoQA.Core.ExtentReport;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Services;
using DemoQA.Core.Extensions;
using DemoQA.Testing.Constants;

namespace DemoQA.Testing.Test.Exercise2
{
    [TestFixture, Category("UpdateUserDetail")]
    public class UpdateUserDetailSchemaTest : BaseTest
    {
        private UserService _userService;

        public UpdateUserDetailSchemaTest()
        {
            _userService = new UserService(ApiClient);
        }

        [Test]
        [TestCase("createdUser", "updatedUser")]
        public async Task UpdateUserDetailSchema(string createAccountKey, string updatedAccountKey)
        {
            CreateUserRequestDTO createdUser = AccountData[createAccountKey];
            UpdateUserRequestDTO updateUser = UpdatedAccountData[updatedAccountKey];

            ReportLog.Info("1. Create User");
            var createdResponse = await _userService.CreateUser(createdUser);
            var createdResult = createdResponse.Data;
            string userId = createdResult.Data.Id;
            _userService.StoreDataToDeleteUser(userId);

            ReportLog.Info("2. Updated User detail by ID");
            var updatedUserResponse = await _userService.UpdateUserDetail(userId, updateUser);

            ReportLog.Info("3. Verify status code");
            updatedUserResponse.VerifyStatusCodeOK();

            ReportLog.Info("4. Verify schema");
            updatedUserResponse.VerifySchema(FileConstant.UpdateUserSchemaPath);
        }

        [TearDown]
        public void AfterUpdateUserDetailTest()
        {
            ReportLog.Info("5. Clear user");
            _userService.DeleteUserFromStorage();
        }

    }
}
