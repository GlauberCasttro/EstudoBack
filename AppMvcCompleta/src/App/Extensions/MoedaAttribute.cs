using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace App.Extensions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MoedaAttribute : ValidationAttribute
    {
        /// <summary>
        /// Atributo para validar uma moeda
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Funcao para colocar o comportamento do cliente na validação do atributo, pode ser usados para varios atributos
    /// </summary>
    public class MoedaAttributeAdpater : AttributeAdapterBase<MoedaAttribute>
    {
        public MoedaAttributeAdpater(MoedaAttribute moedaAttribute, IStringLocalizer stringLocalizer) : base(moedaAttribute, stringLocalizer)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase context)
        {
            return "Moeda em formato inválido.";
        }
    }
    //Classe para fazer o adapter funcionar e tem que ser registrado via injeção de dependencia no startup
    public class MoedaValidationAttributoAdapterProvider : IValidationAttributeAdapterProvider
    {
        private IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if(attribute is MoedaAttribute moedaAttribute)
            {
                return new MoedaAttributeAdpater(moedaAttribute, stringLocalizer);
            }
            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}