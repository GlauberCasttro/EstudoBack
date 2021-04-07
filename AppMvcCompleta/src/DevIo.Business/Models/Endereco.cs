using DevIo.Business.Enums;
using System;

namespace DevIo.Business.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public Estado Estado { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}