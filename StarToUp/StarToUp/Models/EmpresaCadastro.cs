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
        [Key]
        public int EmpresaCadastroID { get; set; }

        [Required(ErrorMessage = "Preencha o nome da Empresa")]
        [DisplayName("Nome:")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome da empresa deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o E-mail de sua Empresa")]
        [DisplayName("E-mail:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Você precisa de uma senha!")]
        [DisplayName("Senha:")]
        public string Senha { get; set; }

        public int TipoUsuarioID { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }

        // Relacionamento perfil empresas
        public virtual ICollection<PerfilEmpresa> PerfilEmpresas { get; set; }
    }
}
