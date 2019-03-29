//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace StarToUp.Models
//{
//    public class Contato
//    {
//        [Key]
//        public int CadastroID { get; set; }

//        [Required(ErrorMessage = "Preencha o seu nome")]
//        [DisplayName("Nome:")]//        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 30 caracteres")]//        public string Nome { get; set; }

//        [Required(ErrorMessage = "Digite o seu e-mail")]
//        [DisplayName("Email:")]
//        public string Email { get; set; }

//        [Required(ErrorMessage = "Você precisa de uma assunto!")]
//        [DisplayName("Assunto:")]
//        [StringLength(20, MinimumLength = 5, ErrorMessage = "O assunto deve ter entre 5 e 20 caracteres")]
//        public string Assunto { get; set; }

//        [Required(ErrorMessage = "Você precisa inserir uma mensagem.")]
//        [DisplayName("Mensagem:")]
//        [StringLength(20, MinimumLength = 5, ErrorMessage = "A mensagem deve ter entre 5 e 20 caracteres")]
//        public string Mensagem { get; set; }
//    }
//}