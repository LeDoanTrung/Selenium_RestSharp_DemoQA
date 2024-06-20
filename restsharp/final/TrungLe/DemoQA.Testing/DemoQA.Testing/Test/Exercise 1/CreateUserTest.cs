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
    [TestFixture, Category("CreateNewUser")]
    public class CreateUserTest : BaseTest
    {
        private UserService _userService;

        public CreateUserTest()
        {
            _userService = new UserService(ApiClient);
        }

        [Test]
        [TestCase("createdUser")]
        public async Task CreateNewUser(string accountKey)
        {
            CreateUserRequestDTO user = AccountData[accountKey];
                                                   
            ReportLog.Info("1. Create User");
            var response = await _userService.CreateUser(user);

            ReportLog.Info("2. Verify status code");
            response.VerifyStatusCodeOK();

            var result = response.Data;

            result.Code.Should().Be(201);
            _userService.StoreDataToDeleteUser(result.Data.Id);

            result.Data.Name.Should().Be(user.Name);
            result.Data.Email.Should().Be(user.Email);
            result.Data.Gender.Should().Be(user.Gender);
            result.Data.Status.Should().Be(user.Status);
        }

        [TearDown]
        public void AfterCreateUserTest()
        {
            ReportLog.Info("4. Clear user");
            _userService.DeleteUserFromStorage();
        }
    }
}
