using Microsoft.AspNetCore.Mvc;
using ApiNet.model;

namespace ApiNet.Controller
{
    [Route("/api[controller]")]
    [ApiController]
    public class Controller(HttpClient httpClient) : ControllerBase
    {
        // GET: /tasks
        [HttpGet("/tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var url = "http://localhost:3001/api/tasks";
            var result = await httpClient.GetAsync(url);

            if (result.StatusCode == System.Net.HttpStatusCode.OK &&
                result.Content.Headers.ContentType?.MediaType == "application/json")
            {
               
                var content = await result.Content.ReadFromJsonAsync<List<TaskDTO>>();
                return Ok(content); // Return 200
            }
            else
            {
                return NotFound(); // Return 404
            }
        }

        // GET: /tasks/{id}
        [HttpGet("/tasks/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var url = $"http://localhost:3001/api/tasks/{id}";
            var result = await httpClient.GetAsync(url);

            if (result.StatusCode == System.Net.HttpStatusCode.OK &&
                result.Content.Headers.ContentType?.MediaType == "application/json")
            {
                var task = await result.Content.ReadFromJsonAsync<TaskDTO>(); 
                if (task != null && task.id == id)
                {
                    return Ok(task); // Return 200
                }
            }

            return NotFound(); // Return 404
        }

        // PUT: /tasks/{id}
        [HttpPut("/tasks/{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO taskRequestDto)
        {
            var url = $"http://localhost:3001/api/tasks/{id}";
            var result = await httpClient.PutAsJsonAsync(url, taskRequestDto);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var task = await result.Content.ReadFromJsonAsync<TaskDTO>(); 
                if (task != null && task.id == id)
                {
                    return Ok(task); // Return 200 
                }
            }

            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent(); // Return 204
            }

            return Problem("Failed to update task", statusCode: 500); //500
        }

        // POST: /tasks
        [HttpPost("/tasks")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO taskRequestDto)
        {
            var url = "http://localhost:3001/api/tasks";
            var result = await httpClient.PostAsJsonAsync(url, taskRequestDto);

            if (result.IsSuccessStatusCode &&
                result.Content.Headers.ContentType?.MediaType == "application/json")
            {
                var createdTask = await result.Content.ReadFromJsonAsync<TaskDTO>(); 
                if (createdTask != null)
                {
                    return CreatedAtAction(
                        nameof(GetTaskById), new { id = createdTask.id },
                        createdTask);
                }

                return Problem("Failed to deserialize response", statusCode: 500);
            }

            return Problem("Failed to create task", statusCode: 500); // Return 500
        }

        // DELETE: /tasks/{id}
        [HttpDelete("/tasks/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var url = $"http://localhost:3001/api/tasks/{id}";
            var result = await httpClient.DeleteAsync(url);

            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent(); // Return 204 
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(); // Return 404 
            }
            else
            {
                return Problem("Failed to delete task", statusCode: 500); // Return 500
            }
        }
    }
}
