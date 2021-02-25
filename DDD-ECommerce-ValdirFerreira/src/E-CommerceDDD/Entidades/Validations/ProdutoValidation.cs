using Flunt.Notifications;
using Flunt.Validations;

namespace Entidades
{
    public class ProdutoValidation : Notifiable, IValidatable
    {
        private Produto Produto { get; set; }
        public ProdutoValidation(Produto produto)
        {
            Produto = produto;
        }
        public void Validate()
        {
            AddNotifications(
                 new Contract()
                  .HasMaxLengthIfNotNullOrEmpty(Produto.Nome, 255, "Nome", $"O campo nome não pode conter mais que 255 caracteres"));
        }
    }
}

