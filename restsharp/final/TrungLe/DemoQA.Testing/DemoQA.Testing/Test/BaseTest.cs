using AventStack.ExtentReports;
using DemoQA.Core.API;
using DemoQA.Core.Configuration;
using DemoQA.Core.Extensions;
using DemoQA.Core.ExtentReport;
using DemoQA.Core.ShareData;
using DemoQA.Core.Util;
using DemoQA.Service.DataObjects;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Services;
using DemoQA.Testing.Constants;
using NUnit.Framework.Interfaces;

namespace DemoQA.Testing.Test
{
    [TestFixture]
    public class BaseTest
    {
        protected Dictionary<string, AccountDTO> AccountData;
        protected static APIClient ApiClient;

        public BaseTest()
        {
            AccountData = JsonFileUtility.ReadAndParse<Dictionary<string, AccountDTO>>(FileConstant.AccountFilePath.GetAbsolutePath());
            ApiClient = new APIClient(ConfigurationHelper.GetConfigurationByKey(TestStartUp.Config,"application:url"));

            //Create parent test report
            ExtentTestManager.CreateParentTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void Setup()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            Console.WriteLine("Base Test Set up");
        }


        [TearDown]
        public void TearDown()
        {
            UpdateTestReport();
            DataStorage.ClearData();
            Console.WriteLine("Base Test Tear Down");
        }

        public void UpdateTestReport()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : TestContext.CurrentContext.Result.StackTrace;
            var message = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case TestStatus.Failed:
                    ReportLog.Fail($"Test failed with message: {message}");
                    ReportLog.Fail($"Stacktrace: {stacktrace}");
                    break;
                case TestStatus.Inconclusive:
                    ReportLog.Skip($"Test inconclusive with message: {message}");
                    ReportLog.Skip($"Stacktrace: {stacktrace}");
                    break;
                case TestStatus.Skipped:
                    ReportLog.Skip($"Test skipped with message: {message}");
                    break;
                default:
                    ReportLog.Pass("Test passed");
                    break;
            }
        }
    }
}
