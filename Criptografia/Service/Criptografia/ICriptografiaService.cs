using Service.Models;
using System.Threading.Tasks;

namespace Service
{
    public interface ICriptografiaService
    {
        Criptografia Crip { get; set; }
        public Task<string> Encrypt(Arquivo arquivo);
        public Task<string> Decrypt(Arquivo arquivo);
    }
}
