using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_Todo_List.Context;
using API_Todo_List.Entities;

namespace API_Todo_List.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public IActionResult Create(Todo todo) 
        {
            _context.Add(todo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = todo.Id}, todo);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var todo = _context.Todos.Find(id);

            if (todo == null)
                return NotFound("Tarefa não encontrada");

            return Ok(todo);
        }

        [HttpGet("GetByDescription")]
        public IActionResult GetByDescription(string description)
        {
            var todo = _context.Todos.Where((todo) => todo.Description.Contains(description));

            if (todo == null)
                return NotFound("Tarefa não encontrada");

            return Ok(todo);
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var todos = _context.Todos.ToList();

            if (todos == null || todos.Count == 0)
                return NotFound("Nenhuma tarefa encontrada");

            return Ok(todos);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Todo todo)
        {
            var todoData = _context.Todos.Find(id);

            if(todoData == null)
                return NotFound("Tarefa não encontrada");

            todoData.Description = todo.Description;
            todoData.Done = todo.Done;

            _context.Todos.Update(todoData);
            _context.SaveChanges();

            return Ok(todoData);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todoData = _context.Todos.Find(id);

            if (todoData == null)
                return NotFound("Tarefa não encontrada");

            _context.Todos.Remove(todoData);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
