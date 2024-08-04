using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acudir.Test.Data.Entities
{
    public class Persona
    {
        public string NombreCompleto { get; set; } = string.Empty; //Tomo el nombre como regitro único.
        public string Edad { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Profesion { get; set; } = string.Empty;
        public bool IsValid()
        {
            //Valido que los campos no sean nulos o vacíos. No hay especificación técnica por lo cual mi decisión es que esten todos los campos presentes.
            bool camposValidos = !string.IsNullOrWhiteSpace(NombreCompleto) &&
                                 !string.IsNullOrWhiteSpace(Edad) &&
                                 !string.IsNullOrWhiteSpace(Domicilio) &&
                                 !string.IsNullOrWhiteSpace(Telefono) &&
                                 !string.IsNullOrWhiteSpace(Profesion);

            //Valido que la edad sea mayor uqe 0
            bool edadValida = int.TryParse(Edad, out int edad) && edad > 0;

            return camposValidos && edadValida;
        }
    }

   

}
