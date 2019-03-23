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
        [DataType(DataType.DateTime, ErrorMessage = "Formato de data inválido")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss tt}")]
        public DateTime DataEvento { get; set; }

        public int AdminID { get; set; }
        public virtual Administrador Administrador { get; set; }
    }
}