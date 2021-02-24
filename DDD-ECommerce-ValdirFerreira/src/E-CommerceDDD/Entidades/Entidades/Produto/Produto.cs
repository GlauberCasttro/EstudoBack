using Flunt.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("Produtos")]
    public class Produto : EntityBase
    {
        [Column("prd_codigo")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Column("prd_nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("prd_valor")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column("prd_situacao")]
        [Display(Name = "Situação")]
        public bool Situacao { get; set; }

        //public override void Validate()
        //{
        //    AddNotifications(new Contract()
        //        .IsNotNullOrEmpty(Nome, "Nome", "O nome do produto é obrigatório"));
        //}
    }
}
