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

        public DbSet<PerfilEmpresa> PerfilEmpresas { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<StartupCadastro> StartupCadastros { get; set; }        public DbSet<PerfilStartup> PerfilStartups { get; set; }        public DbSet<EmpresaCadastro> EmpresaCadastros { get; set; }        public DbSet<Segmentacao> Segmentacoes { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<StarToUp.Models.Administrador> Administradors { get; set; }

        public System.Data.Entity.DbSet<StarToUp.Models.Evento> Eventoes { get; set; }
    }
}