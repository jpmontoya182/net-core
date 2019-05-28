using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvent;
            // AppDomain.CurrentDomain.ProcessExit += (e, s) => Printer.GenerarSonido(2000, 500, 1);
            AppDomain.CurrentDomain.ProcessExit -= AccionDelEvent;


            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.DibujarLinea(20);
            WriteLine($"Nombre : {engine.Escuela.Nombre}, Año de Creacion: {engine.Escuela.AñoCreacion} , Ciudad: {engine.Escuela.Ciudad} , Pais: {engine.Escuela.Pais}");
            // ImprimirCursos(engine.Escuela);
            var listaObjetos = engine.getObjetosEscuela(
                out int conteoEvaluaciones,
                out int conteoAlumnos,
                out int conteoAsignaturas
            );
            // engine.Escuela.LimpiarLugar ();

            var temp = engine.getDiccionarioEscuela();
            engine.ImprimirDiccionario(temp, true);

            var reporteador = new Reporteador(temp);
            var evalList = reporteador.GetListaEvaluaciones();
            var asigList = reporteador.GetListaAsignaturas();
            var evalXasigLista = reporteador.getListaEvaluacionesXAsignatura();
        }

        private static void AccionDelEvent(object sender, EventArgs e)
        {
            Printer.EscribirTitulo("Saliendo ...");
            Printer.GenerarSonido(3000, 500, 1);
        }

        private static void ImprimirCursos(Escuela escuela)
        {
            Printer.EscribirTitulo("Cursos de la escuela");
            // ? valida si el objeto es valido o no 
            if (escuela?.Cursos != null)
            {
                foreach (var item in escuela.Cursos)
                {
                    WriteLine($"Nombre: {item.Nombre}, Id: {item.UniqueId}, Jornada: {item.Jornada}");
                }
            }
        }
    }
}