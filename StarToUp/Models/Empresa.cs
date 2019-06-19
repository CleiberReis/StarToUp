using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Empresa
    {
        [Key]
        public int EmpresaID { get; set; }

        // Dados de login
        [Required(ErrorMessage = "Preencha o nome da Empresa")]
        [DisplayName("Nome:")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome da empresa deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o E-mail de sua Empresa")]
        [DisplayName("E-mail:")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Você precisa de uma senha!")]
        [DisplayName("Senha:")]
        public string Senha { get; set; }

        // Dados de Endereço
        [DisplayName("CEP:")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Digite o nome da rua.")]
        [DisplayName("Rua:")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Digite o nome do bairro.")]
        [DisplayName("Bairro:")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o número, se não houver número digite 'SN'. ")]
        [DisplayName("Número:")]
        public string Numero { get; set; }

        [DisplayName("Complemento:")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Digite em qual cidade sua empresa se encontra. ")]
        [DisplayName("Cidade:")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Digite em qual estado sua empresa se encontra. ")]
        [DisplayName("Estado:")]
        public string Estado { get; set; }

        // Dados da apresentação da empresa
        [DisplayName("Logotipo:")]
        public string Logotipo { get; set; }

        [Required(ErrorMessage = "Digite algo sobre sua empresa. ")]
        [DisplayName("Sobre:")]
        public string Sobre { get; set; }

        [DisplayName("Razão Social:")]
        public string RazaoSocial { get; set; }

        // Midias Sociais
        [DisplayName("Sobre:")]
        public string LinkInstagram { get; set; }
        [DisplayName("Sobre:")]
        public string LinkFacebook { get; set; }
        [DisplayName("Sobre:")]
        public string LinkLinkedin { get; set; }

        [Required(ErrorMessage = "É necessário aceitar os termos de uso antes de se cadastrar. ")]
        [DisplayName("Termo de Uso:")]
        public bool TermoUso { get; set; }

        public string Hash { get; set; }

        // Relacionamentos
        public int SegmentacaoID { get; set; }
        public virtual Segmentacao Segmentacoes { get; set; }
    }

    public class SegmentacaoEmpresaGroup
    {
        public string Descricao { get; set; }
        public int Count { get; set; }
    }
}