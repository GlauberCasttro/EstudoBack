using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Entidades
{
    public abstract class EntityValidation : Notifiable
    {
        public EntityValidation(string nome = null)
        {
            Validar(nome);
        }

        //valida apenas propriedade comum para todas as entidades
        private void Validar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                AddNotifications(
                     new Contract()
                     .IsNotNullOrWhiteSpace(nome, "Nome", "O campo nome é obrigatório")
                     .HasMaxLengthIfNotNullOrEmpty(nome, 255, "Nome", $"O campo nome não pode conter mais que 255 caracteres"));
            }

        }

        public void ValidarEnum<T>(T enumItem, string property)
        {
            if (!Enum.IsDefined(typeof(T), enumItem))
            {
                AddNotification($"{enumItem.GetType().Name}", $"{property} inválido(a).");
            }
        }

    }
}

