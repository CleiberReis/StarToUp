using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarToUp.Repositories
{
    public class DataChart
    {
        public static Dictionary<object, object> SelecionaColunasGraficoPizza()
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            dic.Add("string", "Pessoal");
            dic.Add("number", "Quantidade");
            return dic;
        }
        public static Dictionary<object, object> SelecionaLinhasGraficoPizza()
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            dic.Add("Professores", 63);
            dic.Add("Alunos", 1200);
            dic.Add("Funcionários", 18);
            dic.Add("Estagiários", 12);
            dic.Add("Outras", 6);
            return dic;
        }

        public static Dictionary<object, object> SelecionaLinhasBanco()
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            StarToUp.Models.Context db = new Models.Context();
            var result = (from p in db.StartupCadastros
                          join f in db.Segmentacoes on p.SegmentacaoID equals f.SegmentacaoID
                          group p by p.Nome into g
                          select new
                          {
                              Nome = g.Key,
                              Quantidade =
                              g.Count()
                          }).OrderBy(o => o.Nome);
            foreach (var item in result)
            {
                dic.Add(item.Nome, item.Quantidade);
            }

            return dic;
        }
    }
}