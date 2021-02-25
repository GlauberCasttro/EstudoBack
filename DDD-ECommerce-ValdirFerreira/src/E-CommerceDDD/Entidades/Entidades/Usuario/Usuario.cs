using Entidades.Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    
    public class Usuario : IdentityUser
    {

        [Column(TypeName = "Varchar(11)")]
        [Index("UN_CPF", 0, IsUnique = true)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        public int Idade { get; set; }
        [Column(TypeName = "Varchar(8)")]
        public string Cep { get; set; }
        [Column(TypeName = "Varchar(255)")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Column(TypeName = "Varchar(255)")]
        public string Complemento { get; set; }
        [Column(TypeName = "Varchar(15)")]
        public string Telefone { get; set; }
        [Display(Name = "Situaçao")]
        public bool Situacao { get; set; }
        [Display(Name = "Tipo Usuário")]
        public TipoUsuario Tipo { get; set; }

    }
}
