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
    [TestFixture, Category("GetUserDetail")]
    public class GetUserDetailTest : BaseTest
    {
        private UserService _userService;

        public GetUserDetailTest()
        {
            _userService = new UserService(ApiClient);
        }

        [Test]
        [TestCase("createdUser")]
        public async Task GetUserDetail(string accountKey)
        {
            CreateUserRequestDTO user = AccountData[accountKey];

            ReportLog.Info("1. Create User");
            var createdResponse = await _userService.CreateUser(user);
            var createdResult = createdResponse.Data;
            string userId = createdResult.Data.Id;
            _userService.StoreDataToDeleteUser(userId);

            ReportLog.Info("2. Get User detail by ID");
            var getUserResponse = await _userService.GetUserDetail(userId);
            var getUserResult = getUserResponse.Data;

            ReportLog.Info("3. Verify status code");
            getUserResponse.VerifyStatusCodeOK();

            ReportLog.Info("4. Verify user information");
            getUserResult.Data.Should().BeEquivalentTo(createdResult.Data);

        }


        [TearDown]
        public void AfterGetUserDetailTest()
        {
            ReportLog.Info("5. Clear user");
            _userService.DeleteUserFromStorage();
        }
    }
}
