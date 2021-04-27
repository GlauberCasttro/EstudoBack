using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Formularios.Models;

namespace Formularios.Data
{
    public class FormulariosContext : DbContext
    {
        public FormulariosContext (DbContextOptions<FormulariosContext> options)
            : base(options)
        {
        }

        public DbSet<Formularios.Models.Filme> Filme { get; set; }
    }
}
