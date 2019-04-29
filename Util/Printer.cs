using static System.Console;

namespace CoreEscuela.Util {
    public static class Printer {
        public static void DibujarLinea (int tam = 10) {
            WriteLine ("".PadLeft (tam, '='));
        }

        public static void EscribirTitulo (string titulo) {
            DibujarLinea (titulo.Length);
            WriteLine (titulo);
            DibujarLinea (titulo.Length);
        }

        public static void GenerarSonido (int hz, int tiempo, int cantidad) {
            while (cantidad-- > 0) {
                Beep (hz, tiempo);
            }
        }
    }
}