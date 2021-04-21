using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace App.Extensions
{/// <summary>
    //Essa classe esconde ou mostra um elemento de acordo com as permissoes do usuario
/// O * significa que tipo de elemento esse tagHelper esta atendendo, se for para um html especifico pode passar qual o elemento ex, div, p, form
/// </summary>
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]//Decorando a classe com quais atributos de hltm vai funcionar * sao todos os elementos que decorar o supress-by-claim-name
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]
    public class ApagaElementoByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApagaElementoByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]//o mesmo nome da propriedade do front
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]//Claim de authorização do user
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (temAcesso) return;

            output.SuppressOutput();//se nao tem acesso nao renderiza o elemento, se nao tem permisaao nao gera o elemento
        }
    }

    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class ApagaElementoByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;//Injeta o context para pegar todas as informaçoes do usuario.Meio de acessar o contexto via Http
        //Em qualquer lugar que vc quiser pegar o usuario logado na aplicação é so injetar essa interface htttAcessor

        public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString(); //pegando uma action dentro do request

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }

}