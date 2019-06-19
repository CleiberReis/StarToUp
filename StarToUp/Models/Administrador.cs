using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Administrador
    {
        [Key]
        public int AdministradorID { get; set; }

        [DisplayName("Foto")]
        public string FotoAdmin { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("Nome:")]
        public string NomeAdmin { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("Função:")]
        public string Funcao { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("Login:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite sua Senha")]
        [DisplayName("Senha:")]
        public string Senha { get; set; }
    }
}