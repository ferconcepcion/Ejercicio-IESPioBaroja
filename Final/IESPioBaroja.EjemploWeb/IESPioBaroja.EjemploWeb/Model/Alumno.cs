using System;

namespace IESPioBaroja.EjemploWeb.Model
{
    public class Alumno : ModelBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Dni { get; set; }

    }
}
