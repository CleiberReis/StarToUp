using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class EmpresaCadastro
    {
        public int EmpresaCadastroID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public int TipoUsuarioID { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}