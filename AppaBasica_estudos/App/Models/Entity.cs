using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppaBasica.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
