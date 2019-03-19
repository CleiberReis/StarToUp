using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class EmpresaCadastro
    {
        //[Key]
        //public int EmpresaID { get; set; }
        //[Required(ErrorMessage = "Preencha a descrição da Empresa")]
        //[DisplayName("Descrição")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "A descrição do estado deve ter entre 3 e 50 caracteres.")]
        //public string Descricao { get; set; }
        //[Required(ErrorMessage = "Preencha a sigla do estado")]
        //[StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla do estado deve ter 2 caracteres.")]

      
        public int EmpresaCadastroID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public int TipoUsuarioID { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
