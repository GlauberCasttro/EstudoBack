using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Generics
{
    public interface IGenerics<T> where T : EntityBase
    {
        Task Adicionar(T Object);
        Task Atualizar(T Object);
        Task Remover(T Object);
        Task<T> ObterPorId(Guid Id);
        Task<List<T>> Listar();
    }
}
