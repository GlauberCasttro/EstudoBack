using Flunt.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("Produtos")]
    public class Produto : EntityBase
    {

        [Display(Name = "Código")]
        [Index("UN_ID_PR", 0, IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Column(TypeName = "Varchar(11)")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Situação")]
        public bool Situacao { get; set; }

        [Column(TypeName = "Varchar(255)")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }

        [Column(TypeName = "Varchar(2000)")]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Usuário")]
        public virtual Usuario Usuario { get; set; }
        public string UserId { get; set; }

        #region MyRegion
        //public override void Validate()
        //{
        //    AddNotifications(new Contract()
        //        .IsNotNullOrEmpty(Nome, "Nome", "O nome do produto é obrigatório"));
        //}
        #endregion

        public void AtivarProduto()
        {
            Situacao = true;
        }

    }
}
