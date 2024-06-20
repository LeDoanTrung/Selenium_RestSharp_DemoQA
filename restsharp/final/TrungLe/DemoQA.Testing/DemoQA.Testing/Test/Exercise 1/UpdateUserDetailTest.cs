using FluentAssertions;
using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Testing.Test.Exersize1
{
    [TestFixture, Category("UpdateUserDetail")]
    public class UpdateUserDetailTest : BaseTest
    {
        private UserService _userService;

        public UpdateUserDetailTest()
        {
            _userService = new UserService(ApiClient);
        }

        [Test]
        [TestCase("createdUser","updatedUser")]
        public async Task UpdateUserDetail(string createAccountKey, string updatedAccountKey)
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
            var actualUpdatedUserResult = updatedUserResponse.Data;

            ReportLog.Info("3. Verify status code");
            updatedUserResponse.VerifyStatusCodeOK();

            ReportLog.Info("4. Verify user information");
            actualUpdatedUserResult.Data.Should().BeEquivalentTo(updateUser);
        }

        [TearDown]
        public void AfterUpdateUserDetailTest()
        {
            ReportLog.Info("5. Clear user");
            _userService.DeleteUserFromStorage();
        }
    }
}
