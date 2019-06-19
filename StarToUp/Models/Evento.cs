using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Evento
    {
        [Key]
        public int EventoID { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Foto")]
        public string Foto { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido")]
        public DateTime DataEvento { get; set; }
    }
}