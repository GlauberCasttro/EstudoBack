using Entidades.Entidades;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Validations
{
    public class CompraValidation : EntityValidation, IValidatable
    {
        public CompraUsuario CompraUsuario { get; set; }
        public CompraValidation(CompraUsuario compraUsuario)
        {
            CompraUsuario = compraUsuario;
        }

        public void Validate()
        {

            ValidarEnum(CompraUsuario.Situacao, "Situação");
            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(CompraUsuario.UsuarioId, "Usuario", "É obrigatório informar o usuario da compra"
                ));
        }
    }
}
