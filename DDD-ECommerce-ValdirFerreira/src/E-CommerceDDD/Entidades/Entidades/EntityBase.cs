using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class EntityBase 
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        //public virtual void Validate()
        //{
        //    throw new NotImplementedException("Method not implemented");
        //}
    }
}
