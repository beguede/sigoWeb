using System;
using System.ComponentModel.DataAnnotations;

namespace SigoWeb.Models
{
    public class NormaModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Data de Publicação")]
        public DateTime DataPublicacao { get; set; }
        
        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Comitê")]
        public string Comite { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Idioma")]
        public string Idioma { get; set; }

        [Required]
        [Display(Name = "Organismo")]
        public string Organismo { get; set; }

        [Required]
        [Display(Name = "Objetivo")]
        public string Objetivo { get; set; }
    }
}
