using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Formularios.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Descricao é obrigatório")]
        [StringLength(60, MinimumLength = 2 , ErrorMessage = "O titulo precisa ter entre 60 e 2 caracteres")]
        public string Descricao { get; set; }
     
        [Required(ErrorMessage ="O campo titulo é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo DataLancamento é obrigatório")]
        [Display(Name ="Data de Lançamento")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O campo Avaliacao é obrigatório")]
        public int  Avaliacao { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [Range(1, 1000, ErrorMessage = "O valor precisa estar entre 1 e 1000")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Valor { get; set; }
    }
}
