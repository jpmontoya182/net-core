using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela
{
    // sealed :permite crear instacias pero no heredar
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine() { }

        public void Inicializar()
        {
            Escuela = new Escuela("Universidad de Antioquia", 2019, TipoEscuela.Secundaria, ciudad: "Medellin");
            CargarCursos();
            CargarAsinagturas();
            CargarEvaluaciones();
        }

        // recomendacion para solo lectura IReadOnlyList y  AsReadOnly
        // retornar siempre un generico IEnumerable

        public Dictionary<enumLlavesDiccionario, IEnumerable<ObjectoEscuelaBase>> getDiccionarioEscuela()
        {
            var diccionario = new Dictionary<enumLlavesDiccionario, IEnumerable<ObjectoEscuelaBase>>();
            diccionario.Add(enumLlavesDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(enumLlavesDiccionario.Curso, Escuela.Cursos.Cast<ObjectoEscuelaBase>());

            var listTempAsig = new List<Asignatura>();
            var listTempAlum = new List<Alumno>();
            var listTempEval = new List<Evaluacion>();

            foreach (var cur in Escuela.Cursos)
            {
                listTempAsig.AddRange(cur.Asignatura);
                listTempAlum.AddRange(cur.Alumnos);
                foreach (var alum in cur.Alumnos)
                {
                    listTempEval.AddRange(alum.Evaluaciones);
                }
            }

            diccionario.Add(enumLlavesDiccionario.Evaluacion, listTempEval.Cast<ObjectoEscuelaBase>());
            diccionario.Add(enumLlavesDiccionario.Asignatura, listTempAsig.Cast<ObjectoEscuelaBase>());
            diccionario.Add(enumLlavesDiccionario.Alumno, listTempAlum.Cast<ObjectoEscuelaBase>());
            return diccionario;
        }


        public void ImprimirDiccionario(Dictionary<enumLlavesDiccionario, IEnumerable<ObjectoEscuelaBase>> dic, bool imprimirEvaluaciones = false)
        {
            foreach (var item in dic)
            {
                Printer.EscribirTitulo(item.Key.ToString());

                foreach (var val in item.Value)
                {
                    switch (item.Key)
                    {
                        case enumLlavesDiccionario.Evaluacion:
                            if (imprimirEvaluaciones)
                            {
                                Console.WriteLine(val);
                            }
                            break;
                        case enumLlavesDiccionario.Escuela:
                            Console.WriteLine($"Escuela : {val}");
                            break;
                        case enumLlavesDiccionario.Alumno:
                            Console.WriteLine($"Alumno : {val.Nombre}");
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }

        public IReadOnlyList<ObjectoEscuelaBase> getObjetosEscuela(out int conteoEvaluaciones, out int conteoAlumnos, out int conteoAsignaturas,
            out int conteoCursos, bool incluirEvaluaciones = true, bool incluirAlumnos = true, bool incluirAsignaturas = true, bool incluirCursos = true)
        {
            var listaObj = new List<ObjectoEscuelaBase>();
            conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = 0;
            conteoCursos = 0;

            listaObj.Add(Escuela);
            if (incluirCursos)
            {
                listaObj.AddRange(Escuela.Cursos);
                conteoCursos += Escuela.Cursos.Count;
            }
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignatura.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (incluirAsignaturas)
                {
                    listaObj.AddRange(curso.Asignatura);
                    conteoAsignaturas += curso.Asignatura.Count;
                }

                if (incluirAlumnos)
                {
                    listaObj.AddRange(curso.Alumnos);
                    conteoAlumnos += curso.Alumnos.Count;
                }

                if (incluirEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }

            }
            return listaObj.AsReadOnly();
        }

        public IReadOnlyList<ObjectoEscuelaBase> getObjetosEscuela(bool incluirEvaluaciones = true, bool incluirAlumnos = true,
            bool incluirAsignaturas = true, bool incluirCursos = true)
        {
            return getObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjectoEscuelaBase> getObjetosEscuela(out int conteoEvaluaciones,
            bool incluirEvaluaciones = true, bool incluirAlumnos = true, bool incluirAsignaturas = true, bool incluirCursos = true)
        {
            return getObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjectoEscuelaBase> getObjetosEscuela(out int conteoEvaluaciones, out int conteoAlumnos, bool incluirEvaluaciones = true,
            bool incluirAlumnos = true, bool incluirAsignaturas = true, bool incluirCursos = true)
        {
            return getObjetosEscuela(out conteoEvaluaciones, out conteoAlumnos, out int dummy, out dummy);
        }

        public IReadOnlyList<ObjectoEscuelaBase> getObjetosEscuela(out int conteoEvaluaciones, out int conteoAlumnos, out int conteoAsignaturas,
            bool incluirEvaluaciones = true, bool incluirAlumnos = true, bool incluirAsignaturas = true, bool incluirCursos = true)
        {
            return getObjetosEscuela(out conteoEvaluaciones, out conteoAlumnos, out conteoAsignaturas, out int dummy);
        }

        private List<Alumno> GenerarAlumnos(int cantidad)
        {
            string[] nombre1 = { "Juan", "Ana", "Camilo", "Diana" };
            string[] nombre2 = { "Maria", "Pablo", "Soraida", "Andres" };
            string[] apellido1 = { "Montoya", "Cardona", "Ruiz", "Florez" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        #region Metodos de Cargas
        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignatura)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        private void CargarAsinagturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>() {
                        new Asignatura { Nombre = "Matematicas" },
                        new Asignatura { Nombre = "Educación Fisica" },
                        new Asignatura { Nombre = "Castellano" },
                        new Asignatura { Nombre = "Ciencias Naturales" }

                    };
                curso.Asignatura = listaAsignaturas;
            }
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso> {
                    new Curso() { Nombre = "101", Jornada = TiposJornada.Mañana },
                    new Curso() { Nombre = "201", Jornada = TiposJornada.Mañana },
                    new Curso() { Nombre = "301", Jornada = TiposJornada.Mañana },
                    new Curso() { Nombre = "401", Jornada = TiposJornada.Tarde },
                    new Curso() { Nombre = "501", Jornada = TiposJornada.Tarde }
                };

            Random rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                curso.Alumnos = GenerarAlumnos(cantRandom);
            }
        }

        #endregion

        #region Comentarios  
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
        #endregion

    }
}