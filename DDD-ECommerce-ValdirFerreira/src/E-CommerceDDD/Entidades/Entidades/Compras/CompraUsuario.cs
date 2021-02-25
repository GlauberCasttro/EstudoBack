using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades.Entidades
{
    public class CompraUsuario : EntityBase
    {
        [Display(Name = "Produto")]
        public Produto Produto { get; set; }
        public Guid ProdutoId { get; set; }

        [Display(Name ="Situação Compra")]
        public SituacaoCompra Situacao { get; set; }

        [ForeignKey("Usuario")]
        [Display(Name ="Usuário")]
        public Usuario Usuario { get; set; }
        public string UserId { get; set; }
        public int Quantidade { get; set; }
    }
}
