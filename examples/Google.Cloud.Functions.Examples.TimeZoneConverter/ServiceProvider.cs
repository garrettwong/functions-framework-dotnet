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

using Google.Cloud.Functions.Invoker;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

[assembly: FunctionsServiceProvider(typeof(Google.Cloud.Functions.Examples.TimeZoneConverter.ServiceProvider))]

namespace Google.Cloud.Functions.Examples.TimeZoneConverter
{
    /// <summary>
    /// Service provider to provide the NodaTime built-in TZDB (IANA) date time zone provider
    /// as a dependency to <see cref="Function"/>.
    /// </summary>
    public class ServiceProvider : FunctionsServiceProvider
    {
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services) =>
            services.AddSingleton(DateTimeZoneProviders.Tzdb);
    }
}