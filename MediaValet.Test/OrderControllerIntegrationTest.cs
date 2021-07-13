using Azure.Storage.Queues.Models;
using MediatR;
using Mediavalet.Domain.Commands;
using Mediavalet.Domain.Handlers;
using Mediavalet.Domain.Interfaces;
using Mediavalet.Domain.Queries;
using Mediavalet.Test;
using MediaValet.Core.DTOs;
using MediaValet.Core.Entities;
using MediaValet.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Supervisor.API;
using Supervisor.API.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using Xunit;

namespace MediaValet.Test
{
    public class OrderControllerIntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public OrderControllerIntegrationTest()
        { 
            IWebHostBuilder builder = new WebHostBuilder()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseEnvironment("Development")
             .UseConfiguration(new ConfigurationBuilder()
                  .AddJsonFile("appsettings.test.json") //the file is set to be copied to the output directory if newer
                  .Build()
              ).UseStartup<Startup>();

            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [Fact]
        public void InsertOrderSuccess()
        {
            var order = new OrderDTO
            {
                OrderText = "1 ham burger please"
            };

            var content = JsonConvert.SerializeObject(order);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/order", stringContent).Result;

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public void InsertOrderFails()
        {
            var order = new OrderDTO();
            var content = JsonConvert.SerializeObject(order);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/order", stringContent).Result;

            Assert.True(!response.IsSuccessStatusCode);
        }

        [Fact]
        public void GetOrderConfirmationSuccess()
        {
            var response = _client.GetAsync("/api/order").Result;
            Assert.True(response.IsSuccessStatusCode);
        }

    }
}
