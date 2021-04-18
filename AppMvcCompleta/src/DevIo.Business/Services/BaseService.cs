using DevIo.Business.Models;
using DevIo.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace DevIo.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        /// <summary>
        /// propga os erros até a camada de apresentacao
        /// </summary>
        /// <param name="mensagemErro"></param>
        protected void Notificar(string mensagemErro)
        {
            _notificador.Handle(new Notificacao(mensagemErro));
        }

        /// <summary>
        /// Adiciona os erros para notificar
        /// </summary>
        /// <param name="validationResult"></param>
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        /// <summary>
        ///Metodo que Executa a validação
        /// </summary>
        /// <typeparam name="TV">É a entidade de validação, Ex: FornecedorValidation</typeparam>
        /// <typeparam name="TE">É a entidade de negocio que está sendo validada, Ex: Fornecedor</typeparam>
        /// <returns></returns>
        protected bool ExecutarValidacao<TV, TE>(TV entitidadeValidacao, TE entidadeNegocio) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = entitidadeValidacao.Validate(entidadeNegocio);
            if (validator.IsValid) return true;

            Notificar(validator); 
            
            return false;
        }
    }
}