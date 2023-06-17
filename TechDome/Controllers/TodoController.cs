using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TechDome.DatabaseContext;
using TechDome.Models;

namespace TechDome.Controllers
{
    //[Authorize(Roles = "User, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly MyDatabaseContext _dbContext;

        public TodoController(MyDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var item = _dbContext.Todos.Find(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            var items = _dbContext.Todos.ToList();
            return Ok(items);
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem item)
        {
            _dbContext.Todos.Add(item);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem item)
        {
            var existingItem = _dbContext.Todos.Find(id);
            if (existingItem == null)
                return NotFound();

            existingItem.Title = item.Title;
            existingItem.Description = item.Description;

            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _dbContext.Todos.Find(id);
            if (item == null)
                return NotFound();

            _dbContext.Todos.Remove(item);
            _dbContext.SaveChanges();
            return NoContent();
        }

        }
}
