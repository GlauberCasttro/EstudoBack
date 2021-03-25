using Dominio.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositoryGenerics<T> : IGenerics<T>, IDisposable where T : class
    {
        private readonly ContextBase _contexto;
        protected readonly DbSet<T> DbSet;
        public RepositoryGenerics(ContextBase context)
        {
            _contexto = context;     
            DbSet = _contexto.Set<T>();
        }
        public async Task Adicionar(T Object)
        {
            //using (var data = new ContextBase(_contexto))
            //{
            //    data.Set<T>().Add(Object);
            //    data.Entry(Object).State = EntityState.Added;
            //    await data.SaveChangesAsync();
            //}
        }

        public async Task Atualizar(T Object)
        {
            //using (var data = new ContextBase(_contexto))
            //{
            //    data.Set<T>().Update(Object);
            //    data.Entry(Object).State = EntityState.Modified;
            //    await data.SaveChangesAsync();
            //}
        }

        public async Task<List<T>> Listar()
        {
                return await DbSet.AsNoTracking().ToListAsync();
            
        }

        public async Task<T> ObterPorId(Guid Id)
        {
            //using (var data = new ContextBase(_contexto))
            //{
            //    return await data.Set<T>().FindAsync(Id);
            //}

            return null;
        }

        public async Task Remover(T Object)
        {
        //    using (var data = new ContextBase(_contexto))
        //    {
        //        data.Set<T>().Remove(Object);
        //        data.Entry(Object).State = EntityState.Deleted;
        //        await data.SaveChangesAsync();
        //    }
        }

        #region// Public implementation of Dispose pattern callable by consumers.
        // To detect redundant calls
        private bool _disposed = false;
        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
        #endregion

    }
}
