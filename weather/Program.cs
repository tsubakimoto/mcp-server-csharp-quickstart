﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol;
using System.Net.Http.Headers;

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton(_ =>
{
    // Weather API client
    // var client = new HttpClient() { BaseAddress = new Uri("https://api.weather.gov") };
    // client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("weather-tool", "1.0"));
    // return client;

    // Zipcode API client
    var client = new HttpClient() { BaseAddress = new Uri("https://zipcloud.ibsnet.co.jp") };
    return client;
});

var app = builder.Build();

await app.RunAsync();
