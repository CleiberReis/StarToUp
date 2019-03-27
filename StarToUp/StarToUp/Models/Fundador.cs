using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Fundador
    {
        public int FundadorID { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Foto { get; set; }

        //Relacionamentos
        public virtual ICollection<PerfilStartup> PerfilStartups { get; set; }

    }
}