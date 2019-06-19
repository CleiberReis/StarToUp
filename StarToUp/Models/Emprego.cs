using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Emprego
    {
        [Key]
        public int EmpregoID { get; set; }

        [DisplayName("Título:")]
        public string TituloVaga { get; set; }

        [DisplayName("Local:")]
        public string LocalVaga { get; set; }

        [DisplayName("Salário:")]
        public string SalarioVaga { get; set; }

        [DisplayName("Requisitos:")]
        public string RequisitosVaga { get; set; }

        [DisplayName("Descrição Completa:")]
        public string DescricaoVaga { get; set; }
    }
}