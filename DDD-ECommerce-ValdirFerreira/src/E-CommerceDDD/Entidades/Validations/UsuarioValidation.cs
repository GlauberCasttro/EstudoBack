using Entities.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace Entidades
{
    public class UsuarioValidation : Notifiable, IValidatable
    {
        private ApplicationUser Usuario { get; set; }
        public UsuarioValidation(ApplicationUser usuario)
        {
            Usuario = usuario;
        }
        public void Validate()
        {
            AddNotifications(
                 new Contract()
                  .HasMaxLengthIfNotNullOrEmpty(Usuario.UserName, 255, "Nome", $"O campo nome não pode conter mais que 255 caracteres"));
        }
    }
}
