using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Data;
using todo.Models;

namespace todoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly todoContext _context;

        public TodoesController(todoContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
          if (_context.Todos == null)
          {
              return NotFound();
          }
            return await _context.Todos.ToListAsync();
        }

        // GET: api/Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(long id)
        {
          if (_context.Todos == null)
          {
              return NotFound();
          }
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(long id, Todo todo)
        {
            if (id != todo.ListId)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
          if (_context.Todos == null)
          {
              return Problem("Entity set 'todoContext.Todos'  is null.");
          }
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.ListId }, todo);
        }

        // DELETE: api/Todoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            if (_context.Todos == null)
            {
                return NotFound();
            }
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(long id)
        {
            return (_context.Todos?.Any(e => e.ListId == id)).GetValueOrDefault();
        }
    }
}
