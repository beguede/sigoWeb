using System;
using System.ComponentModel.DataAnnotations;

namespace SigoWeb.Models
{
    public class ConsultoriaModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required]
        [Display(Name = "Id da Empresa")]
        public Guid? EmpresaId { get; set; }

        [Required]
        [Display(Name = "Id da Norma")]
        public Guid? NormaId { get; set; }

        [Required]
        [Display(Name = "Data Inicio")]
        public DateTime? DataInicio { get; set; }

        [Required]
        [Display(Name = "Data Fim")]
        public DateTime? DataFim { get; set; }

        [Required]
        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public virtual NormaModel Norma { get; set; }
        public virtual EmpresaModel Empresa { get; set; }
    }
}
