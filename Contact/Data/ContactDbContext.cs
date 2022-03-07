using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Contact.Models.Entities;

namespace Contact.Data
{
    public class ContactDbContext:DbContext
    {
        public virtual DbSet<UserContact> Contacts { get; set; }

        public ContactDbContext(DbContextOptions<ContactDbContext> options):base(options)
        {
           
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<UserContact>()
        //        .Property(u => u.Id)
        //        .IsRequired();
        //}
       
    }
}
