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
            IList<Segmentacao> segmentacoes = new List<Segmentacao>();
            segmentacoes.Add(new Segmentacao() { Descricao = "Sa�de" });
            segmentacoes.Add(new Segmentacao() { Descricao = "Educa��o" });
            segmentacoes.Add(new Segmentacao() { Descricao = "Tecnologia e Inovaca��o" });
            segmentacoes.Add(new Segmentacao() { Descricao = "Meio Ambiente" });
            foreach (Segmentacao segmentacao in segmentacoes)
            {
                context.Segmentacoes.AddOrUpdate(x => x.SegmentacaoID, segmentacao);
            }
        }
    }
}
