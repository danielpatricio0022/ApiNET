using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiNet.model;
using Xunit;

namespace ApiNet.Tests.api
{
    public class ApiTest
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5119/") ,
            Timeout = TimeSpan.FromSeconds(30)
        };

        [Fact]
        public async Task GetTasks()
        {
            var pingResponse = await httpClient.GetAsync("");
            Assert.True(pingResponse.IsSuccessStatusCode, "A API não está disponível no endereço configurado.");

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
