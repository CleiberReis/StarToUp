using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class PerfilStartup
    {
        // Cabeçalho da empresa
        public int PerfilStartupID { get; set; }

        public string NomeStartup { get; set; }
        public DateTime DataFundacao { get; set; }
        public string TamanhoTime { get; set; }
        
        // Dados de Endereço
        public int Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        // Dados da apresentação da startup
        public string Sobre { get; set; }
        public string Objetivo { get; set; }

        //Imagens da Startup
        public string Logotipo { get; set; }
        public string ImagemLocal1 { get; set; }
        public string ImagemLocal2 { get; set; }
        public string ImagemMVP1 { get; set; }
        public string ImagemMVP2 { get; set; }
        public string ImagemMVP3 { get; set; }
        public string ImagemMVP4 { get; set; }

        // Relacionamentos
        public int StartupCadastroID { get; set; }
        public virtual StartupCadastro StartupCadastro { get; set; }

        public int SegmentacaoID { get; set; }
        public virtual Segmentacao Segmentacoes { get; set; }

        public int FundadorID { get; set; }
        public virtual Fundador Fundadores { get; set; }
    }
}
