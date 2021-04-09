using System;

namespace SigoWeb.Models
{
    public class NormaModel
    {
        public Guid? Id { get; set; }
        public string Codigo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Titulo { get; set; }
        public string Comite { get; set; }
        public string Status { get; set; }
        public string Idioma { get; set; }
        public string Organismo { get; set; }
        public string Objetivo { get; set; }
    }
}
