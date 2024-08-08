using API_Todo_List.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_Todo_List.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
