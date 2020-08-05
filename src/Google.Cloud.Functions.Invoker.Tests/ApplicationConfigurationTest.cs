﻿// Copyright 2020, Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Google.Cloud.Functions.Invoker.Tests
{
    /// <summary>
    /// Tests for the final part of configuring the "application".
    /// </summary>
    public partial class ApplicationConfigurationTest
    {
        [Theory]
        [InlineData("robots.txt")]
        [InlineData("favicon.ico")]
        public async Task NonFunctionPathsGive404(string path)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder
                    .ConfigureFunctionsFrameworkTarget<SimpleFunction>()
                    .ConfigureApplicationForFunctionsFramework()
                    .UseTestServer());
            using var server = await builder.StartAsync();
            using var client = server.GetTestServer().CreateClient();
            using var response = await client.GetAsync(path);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Empty(content);
        }

        public class SimpleFunction : IHttpFunction
        {
            public async Task HandleAsync(HttpContext context)
            {
                await context.Response.WriteAsync("Test message");
            }
        }
    }
}