using Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades.Entidades
{
    [Table("UsuarioCompras")]
    public class CompraUsuario : EntityBase
    {
        [Display(Name = "Produto")]
        public Produto Produto { get; set; }
        public Guid ProdutoId { get; set; }

        [Display(Name ="Situação Compra")]
        public SituacaoCompra Situacao { get; set; }

        [Display(Name ="Usuário")]
        public ApplicationUser Usuario { get; set; }
        public string UsuarioId { get; set; }
        public int Quantidade { get; set; }
    }
}
