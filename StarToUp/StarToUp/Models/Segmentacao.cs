using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Segmentacao
    {
        public int SegmentacaoID { get; set; }
        public string Descricao { get; set; }

        //Relacionamento com Startup
        public virtual ICollection<StartupCadastro> StartupCadastros { get; set; }
    }
}