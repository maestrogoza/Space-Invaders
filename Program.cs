using System;

namespace PrimerJuego
{
    class Program
    {        
        static void Main(string[] args)
        {            
            Console.Title = "Marcianitos";
            Console.CursorVisible = false;

            Juego juego = new Juego();
            Sonido sonido = new Sonido();

            bool terminado, volver;
            volver = true;

            juego.MenuJuego();

            if (!juego.PantallaInicio()) volver = false;

            while (volver)
            {
                terminado = false;

                juego.InicializarJuego();

                while (!terminado)
                {
                    Console.CursorVisible = false;
                    juego.EscribirPantalla();
                    juego.PausaHastaFinalDeFotograma();
                    juego.ComprobarEntradaJuego();
                    juego.AnimarElementos();
                    juego.ComprobarColisiones();
                    terminado = juego.ComprobarPartida();
                    if (terminado) volver = juego.GameOver();
                }
            }
            Console.SetCursorPosition(1, 35);
            Environment.Exit(1);
        }
    }
}
