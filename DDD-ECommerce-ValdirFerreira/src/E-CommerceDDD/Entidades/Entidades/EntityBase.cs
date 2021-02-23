using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Entidades
{
    public class EntityBase : Notifiable, IValidatable
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public virtual void Validate()
        {
            throw new NotImplementedException("Method not implemented");
        }
    }
}
