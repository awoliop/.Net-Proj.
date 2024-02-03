using API.Entities; // Add this line
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly DataContext _context;
        public TodosController(DataContext context) {
            _context= context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Get()
        {
            var todos = await _context.Todos.Find(_ => true).ToListAsync();
            return todos;
        }

        [HttpGet("{id:length(24)}", Name= "GetTodo")]

        public async Task<ActionResult<Todo>> Get(string id) {

            var todo= await _context.Todos.Find(t=>t.Id== id).FirstOrDefaultAsync();
            if(todo == null) return NotFound();  

            return todo;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Create(Todo todo) {

            await _context.Todos.InsertOneAsync(todo);
            return  CreatedAtRoute("GetTodo", new {id= todo.Id}, todo);
        }


        [HttpPut]

        public async Task<IActionResult> Update(string id, Todo todoIn) {

            var filter= Builders<Todo>.Filter.Eq(t=>t.Id, id);
            var update= Builders<Todo>.Update.Set(t=>t.Title, todoIn.Title).Set(t=>t.IsCompleted, todoIn.IsCompleted);

            var result= await _context.Todos.UpdateManyAsync(filter, update);

            if(result.MatchedCount==0) return NotFound();


            return NoContent();
        }   



        [HttpDelete]

        public async Task<IActionResult> Delete(string id) {
            var result= await _context.Todos.DeleteOneAsync(t=>t.Id == id);

            if(result.DeletedCount==0) return NotFound();
            return NoContent();

        }


        
    }
}