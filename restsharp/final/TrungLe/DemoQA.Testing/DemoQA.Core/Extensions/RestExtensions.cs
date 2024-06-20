using FluentAssertions;
using DemoQA.Core.Util;
using Newtonsoft.Json;
using NJsonSchema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Core.Extensions
{
    public static class RestExtensions
    {
        public static async Task VerifySchema(this RestResponse response, string filePath)
        {
            var schema = await JsonSchema.FromJsonAsync(JsonFileUtility.ReadJsonFile(filePath));
            schema.Validate(response.Content).Should().BeEmpty();
        }

        public static dynamic ConvertToDynamicObject(this RestResponse response)
        {
            return (dynamic)JsonConvert.DeserializeObject(response.Content);
        }

        public static void VerifyStatusCodeOK(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public static void VerifyStatusCodeCreated(this RestResponse response)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
