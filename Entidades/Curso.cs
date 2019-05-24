using System;
using System.Collections.Generic;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Curso : ObjectoEscuelaBase, ILugar
    {

        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignatura { get; set; }
        public List<Alumno> Alumnos { get; set; }
        public string Direccion { get; set; }
        public void LimpiarLugar()
        {
            Printer.DibujarLinea();
            Console.WriteLine("Limpiando establecimiento ....");
            Console.WriteLine($"Curso {Nombre} limpio!");
        }
    }
}