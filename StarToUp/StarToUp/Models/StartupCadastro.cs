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
        [DisplayName("Nome:")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail de recado da sua Startup")]
        [DisplayName("Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Você precisa de uma senha!")]
        [DisplayName("Senha:")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }

        //[DisplayName("Data de Cadastro")]
        //[DataType(DataType.DateTime, ErrorMessage = "Formato de data inválido")]
        //[ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss tt}")]
        public DateTime DataCadastro { get; set; }

        // Dados de Endereço
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        // Dados da apresentação/sobre da startup
        public string Sobre { get; set; }
        public string Objetivo { get; set; }
        public DateTime DataFundacao { get; set; }
        public string TamanhoTime { get; set; }

        //Imagens da Startup
        public string Logotipo { get; set; }
        public string ImagemLocal1 { get; set; }
        public string ImagemLocal2 { get; set; }
        public string ImagemMVP1 { get; set; }
        public string ImagemMVP2 { get; set; }
        public string ImagemMVP3 { get; set; }
        public string ImagemMVP4 { get; set; }

        public string LinkInstagram { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkLinkedin { get; set; }

        public bool TermoUso { get; set; }
        public string Hash { get; set; }

        public int SegmentacaoID { get; set; }
        public virtual Segmentacao Segmentacoes { get; set; }
    }

    public class SegmentacaoStartupGroup
    {
        public string Descricao { get; set; }
        public int Count { get; set; }
    }
}