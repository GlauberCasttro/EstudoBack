using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Models;
using System;
using System.Threading.Tasks;

namespace Criptografia.Controllers
{
    public class CriptografiaController : Controller
    {
        private readonly ICriptografiaService _criptografiaService;

        public CriptografiaController(ICriptografiaService criptografiaService)
        {
            _criptografiaService = criptografiaService;
        }

        [HttpPost]
        [Route("api/criptografar")]
        public async Task<IActionResult> CriptografarTexto([FromBody] Arquivo arquivo)
        {
            if (arquivo != null)
            {
                arquivo.Validate();
                if (arquivo.Invalid)
                    return BadRequest(arquivo.Notifications);
            }

            try
            {
                var textoCriptografado = await _criptografiaService.Encrypt(arquivo);

                return Ok(textoCriptografado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new
                    {
                        Message = "Ocorreu um erro.",
                        Error = ex.Message
                    });
            }
        }

        [HttpPost]
        [Route("api/decriptografar")]
        public async Task<IActionResult> DecriptografarTexto([FromBody] Arquivo arquivo)
        {
            if (arquivo != null)
            {
                arquivo.Validate();
                if (arquivo.Invalid)
                    return BadRequest(arquivo.Notifications);
            }

            try
            {
                var textoDecriptografado = await _criptografiaService.Decrypt(arquivo);

                return Ok(textoDecriptografado);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new
                    {
                        Message = "Ocorreu um erro.",
                        Error = ex.Message
                    });
            }
        }
    }
}