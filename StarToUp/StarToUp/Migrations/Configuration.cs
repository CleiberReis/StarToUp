namespace StarToUp.Migrations
{
    using StarToUp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StarToUp.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StarToUp.Models.Context context)
        {
            IList<TipoUsuario> tipoUsuarios = new List<TipoUsuario>();
            tipoUsuarios.Add(new TipoUsuario() { Descricao = "Empresa"});
            tipoUsuarios.Add(new TipoUsuario() { Descricao = "Startup" });
            foreach (TipoUsuario tipoUsuario in tipoUsuarios)
            {
                context.TipoUsuarios.AddOrUpdate(x => x.TipoUsuarioID, tipoUsuario);
            }
        }
    }
}
