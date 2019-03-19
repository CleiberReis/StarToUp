using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarToUp.Models
{
    public class PerfilEmpresa
    {
        // Cabeçalho da empresa
        public int PerfilEmpresaID { get; set; }

        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string SegmentoMercado { get; set; }
        public string QtdFuncionario { get; set; }

        // Dados de Endereço
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        // Dados da apresentação da empresa
        public string Logomarca { get; set; } // Aqui será a foto! 
        public string Objetivo { get; set; }

        // Relacionamento
        public int EmpresaCadastroID { get; set; }
        public virtual EmpresaCadastro EmpresaCadastro { get; set; }
    }
}