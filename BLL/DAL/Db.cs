using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Db : DbContext
    {

        public DbSet<Photo> Photos { get; set; }

        public DbSet<PhotoTypes> PhotoTypes { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<PhotoOwner> PhotoOwners { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Db(DbContextOptions options ) : base(options)
        { 
            
        }    
    }
}
