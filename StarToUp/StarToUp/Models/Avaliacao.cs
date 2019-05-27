using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Avaliacao
    {
        public int AvaliacaoID { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<StartupCadastro> StartupCadastros { get; set; }
    }
}