using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades {
    public class Alumno : ObjectoEscuelaBase {
        public List<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion> ();

    }
}