using Criptografia.Service;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CriptografiaService : ICriptografiaService
    {
        public Criptografia Crip { get; set; }
        public CriptografiaService()
        {
            Crip = new Criptografia(CryptProvider.DES);
        }

        public async Task<string> Encrypt(Arquivo arquivo)
        {
            try
            {
                Crip.Key = arquivo.Key;
                return await Task.Run(() => Crip.Encrypt(arquivo.Texto)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Decrypt(Arquivo arquivo)
        {
            try
            {
                Crip.Key = arquivo.Key;
                return await Task.Run(() => Crip.Decrypt(arquivo.Texto)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
