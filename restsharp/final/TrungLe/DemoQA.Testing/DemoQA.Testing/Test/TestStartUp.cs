using DemoQA.Core.Configuration;
using DemoQA.Core.ExtentReport;
using DemoQA.Core.ShareData;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Testing.Test
{
    [SetUpFixture]
    public class TestStartUp
    {
        public static IConfiguration Config;
        const string AppSettingPath = "Configuration\\appsetting.json";


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            TestContext.Progress.WriteLine("====> Global one time setup");

            // Read Configuration file
            Config = ConfigurationHelper.ReadConfiguration(AppSettingPath);

            DataStorage.InitData();
        }

        [OneTimeTearDown]
        public void End()
        {
            ExtentReportManager.GenerateReport();
        }
    }
}
