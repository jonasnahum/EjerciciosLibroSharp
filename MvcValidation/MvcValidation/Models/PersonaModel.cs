using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcValidation.Models
{
    public class PersonaModel
    {
        [Required(ErrorMessage="Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Range(0,80,ErrorMessage="la edad debe ser entre 0 y 80")]
        public int Edad { get; set; }

    }
}