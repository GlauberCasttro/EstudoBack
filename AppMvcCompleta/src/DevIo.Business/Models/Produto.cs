using System;
using System.Collections.Generic;
using System.Text;

namespace DevIo.Business.Models
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Guid FornecedorId { get; set; }
    }
}
