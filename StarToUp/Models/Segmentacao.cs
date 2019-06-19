using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class Segmentacao
    {
        [Key]
        public int SegmentacaoID { get; set; }

        [Required(ErrorMessage = "A segmentação não pde ficar em branco")]
        [DisplayName("Segmentação:")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "A segmentação deve ter entre 3 e 30 caracteres")]
        public string Descricao { get; set; }

        // Relacionamentos
        public virtual ICollection<Startup> Startups { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}