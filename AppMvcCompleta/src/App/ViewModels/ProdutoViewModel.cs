using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O campo {0} é obrigátotio")]
        public Guid FornecedorId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigátotio")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ", MinimumLength = 2)]
        public string Nome { get; set; }


        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigátotio")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ", MinimumLength = 10)]
        public string Descricao { get; set; }


        //como a imagem nao será salva como string é necessario criar dois campos, 
        //um para mapear na tela e outro para gravar a informação
        public string Imagem { get; set; }

        [DisplayName("Imagem do produto")]
        public IFormFile ImagemUpload { get; set; }

        //--------------DataAnnotations criando para atender as necessidades  dessa aplicação------------------------------------------------------------------
        // [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public FornecedorViewModel Fornecedor { get; set; }

        //Para enviar uma lista de fornecedores para preencher o dropDownList
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
    }
}