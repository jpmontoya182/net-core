using System;

namespace CoreEscuela.Entidades {
    // abstract : permite heredar pero no crear instancias
    public abstract class ObjectoEscuelaBase {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }
        public ObjectoEscuelaBase () => UniqueId = Guid.NewGuid ().ToString ();

    }
}