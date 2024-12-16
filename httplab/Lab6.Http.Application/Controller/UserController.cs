using Lab6.Http.Common;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Http.Application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskApi taskApi;

        public TaskController(ITaskApi taskApi)
        {
            this.taskApi = taskApi;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetAsync(int id)
        {
            var task = await taskApi.GetAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<TaskItem>> GetAsync()
        {
            var tasks = await taskApi.GetAllAsync();

            if (tasks?.Any() != true)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] TaskItem task)
        {
            var result = await taskApi.AddAsync(task);
            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction("Get", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] TaskItem task)
        {
            var result = await taskApi.UpdateAsync(id, task);
            if (!result)
            {
                return BadRequest();
            }

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await taskApi.DeleteAsync(id);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
