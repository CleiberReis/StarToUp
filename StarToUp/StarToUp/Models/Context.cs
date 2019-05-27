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

        public DbSet<StartupCadastro> StartupCadastros { get; set; }        public DbSet<EmpresaCadastro> EmpresaCadastros { get; set; }        public DbSet<Segmentacao> Segmentacoes { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StartupCadastro>()
            .HasMany<Avaliacao>(s => s.Avaliacoes)
            .WithMany(c => c.StartupCadastros)
            .Map(cs =>
            {
                cs.MapLeftKey("StartupCadastroRefId");
                cs.MapRightKey("AvaliacaoRefId");
                cs.ToTable("AvaliaStartup");
            });
            modelBuilder.Conventions.Remove();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<StarToUp.Models.Administrador> Administradors { get; set; }

        public System.Data.Entity.DbSet<StarToUp.Models.Evento> Eventoes { get; set; }
    }
}