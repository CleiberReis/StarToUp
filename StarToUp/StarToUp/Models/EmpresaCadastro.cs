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

        public string RazaoSocial { get; set; }
        public string QtdFuncionario { get; set; }

        // Dados de Endereço
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        // Dados da apresentação da empresa
        public string Logomarca { get; set; }
        public string Objetivo { get; set; }

        public string Hash { get; set; }

        public int SegmentacaoID { get; set; }
        public virtual Segmentacao Segmentacoes { get; set; }

    }
}
