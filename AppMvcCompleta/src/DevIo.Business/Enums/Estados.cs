using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevIo.Business.Enums
{
    public enum Estado
    {
        [Display(Name = "São Paulo")]
        SP,
        [Display(Name = "Rio de Janeiro")]
        RJ,
        [Display(Name = "Minas Gerais")]
        MG,
        [Display(Name = "Paraná")]
        PR,
        [Display(Name = "Santa Catarina")]
        SC,
        [Display(Name = "Rio Grande do Sul")]
        RS,
        [Display(Name = "Bahia")]
        BA,
        [Display(Name = "Pernambuco")]
        PE,
        [Display(Name = "Amazonas")]
        AM,
        [Display(Name = "Pará")]
        PA,
        [Display(Name = "Rondônia")]
        RO,
        [Display(Name = "Mato Grosso")]
        MS
    }
}
