using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiNet.model;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace ApiNet.Tests.api
{
    public class ApiTest : IClassFixture<WebApplicationFactory<Program>> 
    {
        private readonly HttpClient httpClient;

        public ApiTest(WebApplicationFactory<Program> factory) 
        {
            httpClient = factory.CreateClient(); 
        }

        [Fact]
        public async Task GetTasks()
        {
            var response = await httpClient.GetAsync("tasks");
            var data = await response.Content.ReadFromJsonAsync<List<TaskDTO>>();

            if (response.Content.Headers.ContentType != null)
                Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task GetTaskById()
        {
            var response = await httpClient.GetAsync("tasks/3");
            var data = await response.Content.ReadFromJsonAsync<TaskDTO>();

            if (response.Content.Headers.ContentType != null)
                Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(data);
        }

        [Fact]
        public async Task UpdateTask()
        {
            var task = new TaskDTO()
            {
                name = "Task 3",
                description = "Description 3",
                done = false,
                date = DateTime.Now
            };

            var response = await httpClient.PutAsJsonAsync("tasks/3", task);
            var data = await response.Content.ReadFromJsonAsync<TaskDTO>();

            if (response.Content.Headers.ContentType != null)
                Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(data);
        }
    }
}
