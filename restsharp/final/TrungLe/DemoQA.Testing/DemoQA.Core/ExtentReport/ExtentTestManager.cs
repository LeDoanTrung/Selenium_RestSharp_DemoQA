using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Core.ExtentReport
{
    public class ExtentTestManager
    {
        private static AsyncLocal<ExtentTest> _parentTest = new AsyncLocal<ExtentTest>();
        private static AsyncLocal<ExtentTest> _childTest = new AsyncLocal<ExtentTest>();

        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest.Value = ExtentReportManager.Instance.CreateTest(testName, description);
            return _parentTest.Value;
        }

        public static ExtentTest CreateTest(string testName, string description = null)
        {
            _childTest.Value = _parentTest.Value.CreateNode(testName, description);
            return _childTest.Value;
        }

        public static ExtentTest GetTest()
        {
            return _childTest.Value;
        }
    }
}
