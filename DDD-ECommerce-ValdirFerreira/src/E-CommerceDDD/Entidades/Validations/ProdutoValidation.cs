using Entidades.Validations;
using Flunt.Validations;

namespace Entidades
{
    public class ProdutoValidation : EntityValidation, IValidatable
    {
        private Produto Produto { get; set; }
        public ProdutoValidation(Produto produto)
        {
            Produto = produto;
        }
        public void Validate()
        {
            AddNotifications(new Contract()
                .IsLowerThanExtension(Produto.QuantidadeEstoque, 0, "Quantidade Estoque", "Não é possivel ter um produto com estoque 0"
                ));
        }
    }
}

