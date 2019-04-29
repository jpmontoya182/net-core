using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela {
    // sealed :permite crear instacias pero no heredar
    public sealed class EscuelaEngine {
        public Escuela Escuela { get; set; }

        public EscuelaEngine () { }

        public void Inicializar () {
            Escuela = new Escuela ("Universidad de Antioquia", 2019, TipoEscuela.Secundaria, ciudad: "Medellin");
            CargarCursos ();
            CargarAsinagturas ();
            CargarEvaluaciones ();
        }

        private void CargarEvaluaciones () {
            foreach (var curso in Escuela.Cursos) {
                foreach (var asignatura in curso.Asignatura) {
                    foreach (var alumno in curso.Alumnos) {
                        var rnd = new Random (System.Environment.TickCount);
                        for (int i = 0; i < 5; i++) {
                            var ev = new Evaluacion {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i+1}",
                                Nota = (float) (5 * rnd.NextDouble ()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add (ev);
                        }
                    }
                }
            }
        }

        private void CargarAsinagturas () {
            foreach (var curso in Escuela.Cursos) {
                List<Asignatura> listaAsignaturas = new List<Asignatura> () {
                    new Asignatura { Nombre = "Matematicas" },
                    new Asignatura { Nombre = "Educación Fisica" },
                    new Asignatura { Nombre = "Castellano" },
                    new Asignatura { Nombre = "Ciencias Naturales" }

                };
                curso.Asignatura = listaAsignaturas;
            }
        }

        private void CargarCursos () {
            Escuela.Cursos = new List<Curso> {
                new Curso () { Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso () { Nombre = "201", Jornada = TiposJornada.Mañana },
                new Curso () { Nombre = "301", Jornada = TiposJornada.Mañana },
                new Curso () { Nombre = "401", Jornada = TiposJornada.Tarde },
                new Curso () { Nombre = "501", Jornada = TiposJornada.Tarde }
            };

            Random rnd = new Random ();
            foreach (var curso in Escuela.Cursos) {
                int cantRandom = rnd.Next (5, 20);
                curso.Alumnos = GenerarAlumnos (cantRandom);
            }

        }

        private List<Alumno> GenerarAlumnos (int cantidad) {
            string[] nombre1 = { "Juan", "Ana", "Camilo", "Diana" };
            string[] nombre2 = { "Maria", "Pablo", "Soraida", "Andres" };
            string[] apellido1 = { "Montoya", "Cardona", "Ruiz", "Florez" };

            var listaAlumnos = from n1 in nombre1
            from n2 in nombre2
            from a1 in apellido1
            select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy ((al) => al.UniqueId).Take (cantidad).ToList ();
        }

        /*
            private static bool Predicado (Curso obj) {
                return obj.Nombre == "301";
            }
            
            escuela.Cursos.AddRange (otraColeccion);
            ImprimirCursos (escuela);
            // Delegado : determina los parametros de entrada y salida 
            escuela.Cursos.RemoveAll (delegate (Curso cur) { return cur.Nombre == "301"; });
            //  expression lambda y delegados 
            escuela.Cursos.RemoveAll ((cur) => cur.Nombre == "501" && cur.Jornada == TiposJornada.Tarde);
            */

    }
}