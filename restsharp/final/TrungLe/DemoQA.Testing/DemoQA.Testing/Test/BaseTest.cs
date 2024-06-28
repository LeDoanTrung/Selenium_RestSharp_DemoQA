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
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;

namespace DemoQA.Testing.Test
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class BaseTest
    {
        protected Dictionary<string, AccountDTO> AccountData;
        protected Dictionary<string, BookDTO> BookData;
        protected static APIClient ApiClient;
        public BaseTest()
        {
            AccountData = JsonFileUtility.ReadAndParse<Dictionary<string, AccountDTO>>(FileConstant.AccountFilePath.GetAbsolutePath());
            BookData = JsonFileUtility.ReadAndParse<Dictionary<string, BookDTO>>(FileConstant.BookFilePath.GetAbsolutePath());
            ApiClient = new APIClient(ConfigurationHelper.GetConfigurationByKey(TestStartUp.Config,"application:url"));

            //Create parent test report
            ExtentTestManager.CreateParentTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void Setup()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            DataStorage.InitData();
            Console.WriteLine("Base Test Set up");
        }


        [TearDown]
        public void TearDown()
        {
            ExtentTestManager.UpdateTestReport();
            DataStorage.ClearData();
            Console.WriteLine("Base Test Tear Down");
        }

    }
}
