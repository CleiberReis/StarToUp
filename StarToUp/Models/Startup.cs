using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Startup
    {
        [Key]
        public int StartupID { get; set; }

        // Dados de login

        [Required(ErrorMessage = "Preencha o nome da sua Startup")]
        [DisplayName("Nome:")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail de recado da sua Startup")]
        [DisplayName("E-mail:")]
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

        // Dados da apresentação/sobre da startup
        [Required(ErrorMessage = "Digite algo sobre sua empresa. ")]
        [DisplayName("Sobre:")]
        public string Sobre { get; set; }

        [DisplayName("Data de Fundação")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        public DateTime DataFundacao { get; set; }


        //Imagens da Startup
        [DisplayName("Logotipo:")]
        public string Logotipo { get; set; }

        [DisplayName("Imagem da Startup:")]
        public string ImgStartup1 { get; set; }

        [DisplayName("Imagem da Startup:")]
        public string ImgStartup2 { get; set; }

        [DisplayName("Imagem do Produto:")]
        public string ImagemMVP1 { get; set; }

        [DisplayName("Imagem do Produto:")]
        public string ImagemMVP2 { get; set; }
    

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

        public int AvaliacaoID { get; set; }
        public virtual Avaliacao Avaliacoes { get; set; }

    }

    public class SegmentacaoStartupGroup
    {
        public string Descricao { get; set; }
        public int Count { get; set; }
    }
}
