using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class UsuarioValidation : Notifiable, IValidatable
    {
        private Usuario Usuario { get; set; }
        public UsuarioValidation(Usuario usuario)
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
