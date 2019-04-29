using System.Collections.Generic;

namespace CoreEscuela.Entidades {
    public class Escuela : ObjectoEscuelaBase {
        private string nombre;
        public int AñoCreacion { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public TipoEscuela TipoEscuela { get; set; }
        public List<Curso> Cursos { get; set; }

        #region Contructor
        // igualacion por tuplas
        public Escuela (string nombre, int año) => (Nombre, AñoCreacion) = (nombre, año);
        public Escuela (string nombre, int año,
            TipoEscuela tipo,
            string pais = "", string ciudad = "") {
            // Igualacion por tuplas
            (Nombre, AñoCreacion) = (nombre, año);
            Pais = pais;
            TipoEscuela = tipo;
            Ciudad = ciudad;
            // this cuando son iguales, para determinar de cual estamos hablando
        }
        #endregion

        // modificamos .ToString()
        public override string ToString () {
            return $"Nombre: {Nombre}, Tipo: {TipoEscuela} Año: {AñoCreacion} \nPais: {Pais}, Ciudad: {Ciudad}";
        }
    }
}