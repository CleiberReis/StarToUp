using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class TipoUsuario
    {
        public int TipoUsuarioID { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<StartupCadastro> StartupCadastros { get; set; }
    }
}