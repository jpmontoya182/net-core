using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela {
    class Program {
        static void Main (string[] args) {
            var engine = new EscuelaEngine ();
            engine.Inicializar ();
            Printer.DibujarLinea (20);
            WriteLine ($"Nombre : {engine.Escuela.Nombre}, Año de Creacion: {engine.Escuela.AñoCreacion} , Ciudad: {engine.Escuela.Ciudad} , Pais: {engine.Escuela.Pais}");
            ImprimirCursos (engine.Escuela);
            var listaObjetos = engine.getObjetosEscuela ();

            var listILugar = from obj in listaObjetos
            where obj is ILugar
            select obj;

            engine.Escuela.LimpiarLugar ();

        }

        private static void ImprimirCursos (Escuela escuela) {
            Printer.EscribirTitulo ("Cursos de la escuela");
            // ? valida si el objeto es valido o no 
            if (escuela?.Cursos != null) {
                foreach (var item in escuela.Cursos) {
                    WriteLine ($"Nombre: {item.Nombre}, Id: {item.UniqueId}, Jornada: {item.Jornada}");
                }
            }

        }
    }

}