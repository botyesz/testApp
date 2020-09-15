using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace LynxTestApp.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext
    {

        public DataContext()
            : base("DefaultConnection")
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<User> Users { get; set; }

    }
}