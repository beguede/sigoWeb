using System;
using System.ComponentModel.DataAnnotations;

namespace SigoWeb.Models
{
    public class EmpresaModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        [Required]
        [Display(Name = "Ativa")]
        public bool Ativa { get; set; }
    }
}
