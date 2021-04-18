using DevIo.Business.Models;
using DevIo.Business.Models.Enuns;
using FluentValidation;
using System.Text.RegularExpressions;
using static DevIo.Business.Validations.Documentos.ValidacaoDocs;

namespace DevIo.Business.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyValue} precisa ser preenchido.")
                .Length(2, 100).WithMessage("O campo {PropertyValue} precisa ter entre {MinLength} e {maxLength} caracteres");

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O campo documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
            });

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                   .WithMessage("O campo documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}