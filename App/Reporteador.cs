using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<enumLlavesDiccionario, IEnumerable<ObjectoEscuelaBase>> _dicObjEscuela;
        public Reporteador(Dictionary<enumLlavesDiccionario, IEnumerable<ObjectoEscuelaBase>> dicObjEscuela)
        {
            if (dicObjEscuela == null)
            {
                throw new ArgumentNullException(nameof(dicObjEscuela));
            }

            _dicObjEscuela = dicObjEscuela;
        }
        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            if (_dicObjEscuela.TryGetValue(enumLlavesDiccionario.Evaluacion, out IEnumerable<ObjectoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            else
            {
                return new List<Evaluacion>();
            }
        }
        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> getListaEvaluacionesXAsignatura()
        {
            var result = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listaAsignaturas = GetListaAsignaturas(out var listEvaluaciones);


            foreach (var asign in listaAsignaturas)
            {
                var evalAsign = from eval in listEvaluaciones
                                where eval.Asignatura.Nombre == asign
                                select eval;
                result.Add(asign, evalAsign);
            }

            return result;
        }
    }
}