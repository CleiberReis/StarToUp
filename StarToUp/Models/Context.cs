using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Context : DbContext
    {
        public Context() : base("StarToUp")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
        }

        public DbSet<Startup> Startups { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Segmentacao> Segmentacoes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Emprego> Empregos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}