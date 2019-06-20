using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Avaliacao
    {
        [Key]
        public int AvaliacaoID { get; set; }

        [DisplayName("Descrição da avaliação:")]
        public string Descricao { get; set; }

        // Relacionamento
        public virtual ICollection<StartupCadastro> StartupCadastros { get; set; }
    }
}