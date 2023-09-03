using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace to_do_List.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}