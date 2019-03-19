using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class StartupCadastro
    {
        [Key]
        public int StartupCadastroID { get; set; }

        [Required(ErrorMessage = "Preencha o nome da sua Startup")]
        [DisplayName("Nome:")]        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 30 caracteres")]        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail de recado da sua Startup")]
        [DisplayName("Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Você precisa de uma senha!")]
        [DisplayName("Senha:")]
        public string Senha { get; set; }

        public int TipoUsuarioID { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}