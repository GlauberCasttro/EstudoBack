using Flunt.Validations;

namespace Entidades.Validations
{
    public static class ValidationExtensions
    {
        public static Contract IsLowerThanExtension(this Contract contract, int valor, int comparado, string propriedade, string mensagem)
        {
            if(valor < comparado)
            {
                contract.AddNotification(propriedade, mensagem);
            }

            return contract;
        }

        public static Contract IsLowerThanExtension(this Contract contract, double valor, double comparado, string propriedade, string mensagem)
        {
            if (valor <= comparado)
            {
                contract.AddNotification(propriedade, mensagem);
            }

            return contract;
        }
    }
}
