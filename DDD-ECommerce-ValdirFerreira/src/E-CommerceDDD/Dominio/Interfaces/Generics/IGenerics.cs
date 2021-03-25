using Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Generics
{
    public interface IGenerics<T> where T : class
    {
        Task Adicionar(T Object);
        Task Atualizar(T Object);
        Task Remover(T Object);
        Task<T> ObterPorId(Guid Id);
        Task<List<T>> Listar();
    }
}
