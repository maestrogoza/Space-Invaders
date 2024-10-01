using System;
using System.Threading;

namespace PrimerJuego
{
    class Juego
    {
        Random generador = new Random();

        static Sprite[] enemigos= new Sprite[4];
        static Sprite[] corazones = new Sprite[2];
        static Sprite[] municion = new Sprite[2];
        static Sprite[] nitro = new Sprite[2];
        static Sprite personaje;
        static Sprite escudo;
        static Sprite balaPersonaje;
        static Sprite balaEnemigo;
        static Sprite llaveMaestra;

        static int xFlecha, yFlecha;
        static int anchoPantalla = 50;
        static int altoPantalla = 35;
        static int xCentroPantalla = anchoPantalla/2 - 1;
        static int yCentroPantalla = altoPantalla / 2 - 1;
        static int xInicialPersonaje = xCentroPantalla;
        static int yInicialPersonaje = Convert.ToInt32(yCentroPantalla*1.5);
        static int yBloqueEnemigos = Convert.ToInt32(yCentroPantalla);
        static int yBloqueJuego = Convert.ToInt32(yCentroPantalla * 1.5);
        static int vidas, balasPersonaje, enemigosRestantes;
        static string estadoEscudo;
        static string estadoNitro;
        static bool terminado;
        static int enemigoDispara;

        static ConsoleKeyInfo tecla;

        public void MenuJuego()
        {
            Console.SetWindowSize(anchoPantalla+2, altoPantalla+1);
            Console.SetCursorPosition(xCentroPantalla, yCentroPantalla);
            Console.WriteLine("NEW GAME");
            Console.SetCursorPosition(xCentroPantalla+2, yCentroPantalla + 1);
            Console.WriteLine("EXIT");
        }

        public bool PantallaInicio()
        {
            xFlecha = xCentroPantalla-2;
            yFlecha = yCentroPantalla;

            Console.SetCursorPosition(xFlecha, yFlecha);
            Console.WriteLine((char)16);
            do
            {
                tecla = Console.ReadKey();
                if (tecla.Key == ConsoleKey.DownArrow && yFlecha == yCentroPantalla)
                {
                    BorrarFlecha();
                    yFlecha++;
                }
                else if (tecla.Key == ConsoleKey.UpArrow && yFlecha == yCentroPantalla +1)
                {
                    BorrarFlecha();
                    yFlecha--;
                }
                EscribirFlecha(xFlecha, yFlecha);
            } while (tecla.Key != ConsoleKey.Enter);
            if (yFlecha == yCentroPantalla) return true;
            else return false;
        }
        private void BorrarFlecha()
        {
            Console.SetCursorPosition(xFlecha, yFlecha);
            Console.WriteLine(" ");
        }
        private void EscribirFlecha(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine((char)16);
        }        

        public void InicializarJuego()
        {
            terminado = false;
            BorrarPantalla();
            Console.SetCursorPosition(xCentroPantalla-5, yCentroPantalla);
            Console.WriteLine("GAME STARTS IN...");
            for (int i = 3; i > 0; i--)
            {                
                Console.SetCursorPosition(xCentroPantalla+2, yCentroPantalla+2);
                Console.WriteLine("          ");
                Console.SetCursorPosition(xCentroPantalla+2, yCentroPantalla+2);
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
            BorrarPantalla();

            enemigosRestantes = enemigos.Length;
            balasPersonaje = 100;
            estadoEscudo = "OFF";
            estadoNitro = "OFF";
            vidas = 3;

            personaje = new Sprite();
            personaje.x = xInicialPersonaje;
            personaje.y = yInicialPersonaje;
            personaje.velocidad = 1;
            personaje.color = ConsoleColor.Cyan;
            personaje.simbolo = (char)30;
            personaje.visible = true;

            llaveMaestra = new Sprite();
            llaveMaestra.x = xCentroPantalla+1;
            llaveMaestra.y = 2;
            llaveMaestra.simbolo = (char)21;
            llaveMaestra.visible = true;
            llaveMaestra.color = ConsoleColor.Yellow;

            balaPersonaje = new Sprite();
            balaPersonaje.x = xInicialPersonaje;
            balaPersonaje.y = yInicialPersonaje+1;
            balaPersonaje.velocidad = 1;
            balaPersonaje.color = ConsoleColor.DarkYellow;
            balaPersonaje.simbolo= '"';
            balaPersonaje.visible = false;

            balaEnemigo = new Sprite();
            balaEnemigo.simbolo = '"';//(char)4;
            balaEnemigo.visible = false;
            balaEnemigo.velocidad = 1;
            balaEnemigo.color = ConsoleColor.DarkGreen;

            escudo = new Sprite();
            escudo.x= generador.Next(2, anchoPantalla-1);
            escudo.y= generador.Next(yBloqueJuego, altoPantalla-2);
            escudo.color = ConsoleColor.DarkGray;
            escudo.simbolo = '#';
            escudo.visible = true;

            for (int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i] = new Sprite();
                enemigos[i].x = generador.Next(3, anchoPantalla-1);
                enemigos[i].y = generador.Next(3, yBloqueEnemigos);
                enemigos[i].velocidad = 1;
                enemigos[i].color = ConsoleColor.DarkGreen;
                enemigos[i].simbolo= (char)165;
                enemigos[i].visible = true;
            }

            for (int i = 0; i < corazones.Length; i++)
            {
                corazones[i] = new Sprite();
                corazones[i].x = generador.Next(2, anchoPantalla-1);
                corazones[i].y = generador.Next(yBloqueJuego, altoPantalla-2);
                corazones[i].color = ConsoleColor.Red;
                corazones[i].simbolo = (char)3;
                corazones[i].visible = true;
            }

            for (int i = 0; i < municion.Length; i++)
            {
                municion[i] = new Sprite();
                municion[i].x = generador.Next(2, anchoPantalla-1);
                municion[i].y = generador.Next(yBloqueJuego, altoPantalla-2);
                municion[i].color = ConsoleColor.DarkYellow;
                municion[i].simbolo = (char)34;
                municion[i].visible = true;
            }

            for (int i = 0; i < nitro.Length; i++)
            {
                nitro[i] = new Sprite();
                nitro[i].x = generador.Next(2, anchoPantalla - 1);
                nitro[i].y = generador.Next(yBloqueJuego, altoPantalla - 2);
                nitro[i].color = ConsoleColor.Blue;
                nitro[i].simbolo = (char)29;
                nitro[i].visible = true;
            }
        }
        public void BorrarPantalla()
        {
            Console.Clear();
        }
        public void EliminarEnemigos()
        {
            Console.SetCursorPosition(1, 1);
            for (int i = 0; i < yBloqueEnemigos; i++)
            {
                Console.WriteLine("                                                             ");
            }
        }
        public void EliminarLlave()
        {
            Console.SetCursorPosition(llaveMaestra.x, llaveMaestra.y);
            Console.WriteLine(" ");
        }
        public void EliminarPersonaje()
        {
            Console.SetCursorPosition(personaje.x,personaje.y);
            Console.WriteLine(" ");
        }
        public void EliminarCorazones()
        {
            for (int i = 0; i < corazones.Length; i++)
            {
                Console.SetCursorPosition(corazones[i].x, corazones[i].y);
                Console.WriteLine(" ");
            }
        }
        public void EliminarNitro()
        {
            for (int i = 0; i < nitro.Length; i++)
            {
                Console.SetCursorPosition(nitro[i].x, nitro[i].y);
                Console.WriteLine(" ");
            }
        }
        public void EliminarMunicion()
        {
            for (int i = 0; i < municion.Length; i++)
            {
                Console.SetCursorPosition(municion[i].x, municion[i].y);
                Console.WriteLine(" ");
            }
        }
        public void EliminarEscudo()
        {
            Console.SetCursorPosition(escudo.x, escudo.y);
            Console.WriteLine(" ");
        }
        public void EliminarInfo()
        {
            Console.SetCursorPosition(1, altoPantalla - 1);
            Console.Write("                                                             ");
        }
        public void EliminarBalaEnemigo()
        {
            Console.SetCursorPosition(balaEnemigo.x, balaEnemigo.y - 1);
            Console.WriteLine(" ");
        }
        public void EliminarBalaPersonaje()
        {
            Console.SetCursorPosition(balaPersonaje.x, balaPersonaje.y+1);
            Console.WriteLine(" ");            
        }
        public void EscribirPantalla()
        {
            //Dibujar en pantalla            
            EliminarEnemigos();            
            EliminarInfo();

            Console.SetCursorPosition(llaveMaestra.x, llaveMaestra.y);
            Console.ForegroundColor = llaveMaestra.color;
            Console.WriteLine(llaveMaestra.simbolo);

            Console.SetCursorPosition(personaje.x, personaje.y);
            Console.ForegroundColor = personaje.color;
            Console.WriteLine(personaje.simbolo);

            for (int i = 0; i < enemigos.Length; i++)
            {
                if (enemigos[i].visible)
                {
                    Console.SetCursorPosition(enemigos[i].x, enemigos[i].y);
                    Console.ForegroundColor = enemigos[i].color;
                    Console.WriteLine(enemigos[i].simbolo);
                }
            }

            if (balaPersonaje.visible)
            {                
                Console.SetCursorPosition(balaPersonaje.x, balaPersonaje.y);
                Console.ForegroundColor = balaPersonaje.color;
                Console.WriteLine(balaPersonaje.simbolo);
            }

            if (balaEnemigo.visible)
            {
                Console.SetCursorPosition(balaEnemigo.x, balaEnemigo.y);
                Console.ForegroundColor = balaEnemigo.color;
                Console.WriteLine(balaEnemigo.simbolo);
            }

            for (int i = 0; i < corazones.Length; i++)
            {
                if (corazones[i].visible)
                {
                    Console.SetCursorPosition(corazones[i].x, corazones[i].y);
                    Console.ForegroundColor = corazones[i].color;
                    Console.WriteLine(corazones[i].simbolo);
                }
            }

            for (int i = 0; i < nitro.Length; i++)
            {
                if (nitro[i].visible)
                {
                    Console.SetCursorPosition(nitro[i].x, nitro[i].y);
                    Console.ForegroundColor = nitro[i].color;
                    Console.WriteLine(nitro[i].simbolo);
                }
            }

            for (int i = 0; i < municion.Length; i++)
            {
                if (municion[i].visible)
                {
                    Console.SetCursorPosition(municion[i].x, municion[i].y);
                    Console.ForegroundColor = municion[i].color;
                    Console.WriteLine(municion[i].simbolo);
                }
            }

            if (escudo.visible)
            {
                Console.SetCursorPosition(escudo.x, escudo.y);
                Console.ForegroundColor = escudo.color;
                Console.WriteLine(escudo.simbolo);
            }
            EscribirInfo();
        }
        public void EscribirInfo()
        {
            Console.SetCursorPosition(1, altoPantalla - 1);
            for (int i = 0; i < vidas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" {0}",(char)3);
            }
            Console.SetCursorPosition(8, altoPantalla - 1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" Bullets x{0}", balasPersonaje);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" Shield {0}", estadoEscudo);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" Enemies {0}", enemigosRestantes);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Nitro {0}", estadoNitro);
        }

        public void ComprobarEntradaJuego()
        {
            //Control del personaje
            if (Console.KeyAvailable)
            {
                tecla = Console.ReadKey();
                if (tecla.Key == ConsoleKey.UpArrow && personaje.y > 2) { EliminarPersonaje(); personaje.y-=personaje.velocidad; }
                else if (tecla.Key == ConsoleKey.DownArrow && personaje.y < altoPantalla - 3) { EliminarPersonaje(); personaje.y += personaje.velocidad; }
                else if (tecla.Key == ConsoleKey.LeftArrow && personaje.x > 2) { EliminarPersonaje(); personaje.x -= personaje.velocidad; }
                else if (tecla.Key == ConsoleKey.RightArrow && personaje.x < anchoPantalla) { EliminarPersonaje(); personaje.x += personaje.velocidad; }
                else if (tecla.Key == ConsoleKey.Spacebar && balasPersonaje > 0 && !balaPersonaje.visible) DispararPersonaje();
                else if (tecla.Key == ConsoleKey.Escape) Pausar();
            }
        }
        public void Pausar()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xCentroPantalla-3, yCentroPantalla);
            Console.WriteLine("GAME PAUSED");
            Console.SetCursorPosition(xCentroPantalla-13, yCentroPantalla + 2);
            Console.WriteLine("·ESC to Exit  ·SPACEBAR to Resume");
            do
            {
                tecla = Console.ReadKey();

            } while (tecla.Key != ConsoleKey.Escape && tecla.Key != ConsoleKey.Spacebar);
            if (tecla.Key == ConsoleKey.Escape) terminado = true;
            else BorrarPantalla();
        }
        public void DispararPersonaje()
        {
            balaPersonaje.x = personaje.x;
            balaPersonaje.y = personaje.y-1;
            balaPersonaje.visible = true;
            balasPersonaje--;
        }

        public void DispararEnemigos()
        {
            bool condicionDisparo = false;
            enemigoDispara = generador.Next(0, enemigos.Length);
            if (enemigos[enemigoDispara].visible) condicionDisparo = true;
            while (!condicionDisparo)
            {
                enemigoDispara = generador.Next(0, enemigos.Length);
                if (enemigos[enemigoDispara].visible) condicionDisparo = true;
            }
            if(enemigos[enemigoDispara].x==personaje.x)
            {
                balaEnemigo.x = enemigos[enemigoDispara].x;
                balaEnemigo.y = enemigos[enemigoDispara].y;
                balaEnemigo.visible = true;
            }
        }

        public void AnimarElementos()
        {
            //Animación movimiento enemigos
            for (int i = 0; i < enemigos.Length; i++)
            {
                if (enemigos[i].x > anchoPantalla - 2 || enemigos[i].x < 3) enemigos[i].velocidad = -enemigos[i].velocidad;
                enemigos[i].x += enemigos[i].velocidad;
            }

            //Animación movimiento balas personaje
            if (balaPersonaje.visible)
            {
                EliminarBalaPersonaje();
                balaPersonaje.y -= balaPersonaje.velocidad;
            }

            //Animación movimiento balas enemigo
            if (balaEnemigo.visible)
            {
                EliminarBalaEnemigo();
                balaEnemigo.y += balaEnemigo.velocidad;
            }

            if (!balaEnemigo.visible) DispararEnemigos();
        }

        public void ComprobarColisiones()
        {
            //Colisiones enemigos
            for (int i = 0; i < enemigos.Length; i++)
            {
                if ((enemigos[i].x == personaje.x && enemigos[i].y == personaje.y) && enemigos[i].visible && estadoEscudo == "OFF")
                {
                    estadoEscudo = "OFF";
                    estadoNitro = "OFF";
                    vidas--;
                    personaje.x = xInicialPersonaje;
                    personaje.y = yInicialPersonaje;
                    personaje.velocidad = 1;
                }
                else if ((enemigos[i].x == personaje.x && enemigos[i].y == personaje.y) && enemigos[i].visible && estadoEscudo == "ON")
                {
                    enemigos[i].visible = false;
                    estadoEscudo = "OFF";
                    enemigosRestantes--;
                    personaje.velocidad = 1;
                }
            }

            //Colisiones balas del personaje
            for (int i = 0; i < enemigos.Length; i++)
            {
                if (enemigos[i].x == balaPersonaje.x && enemigos[i].y == balaPersonaje.y) { enemigos[i].visible = false; enemigosRestantes--; }
                if (balaPersonaje.y < 1)
                {
                    balaPersonaje.visible = false;
                }
            }

            //Colisiones balas del enemigo                        
            if (balaEnemigo.x == personaje.x && balaEnemigo.y == personaje.y && estadoEscudo == "ON")
            {
                estadoEscudo = "OFF";
                personaje.velocidad = 1;
            }
            else if (balaEnemigo.x == personaje.x && balaEnemigo.y == personaje.y && estadoEscudo == "OFF") {vidas--; personaje.velocidad = 1; estadoNitro = "OFF"; }
            if (balaEnemigo.y >= altoPantalla-2)
            {
                balaEnemigo.visible = false;
            }
            
            //Colisiones corazones
            for (int i = 0; i < corazones.Length; i++)
            {
                if(corazones[i].x == personaje.x && corazones[i].y==personaje.y && corazones[i].visible)
                {
                    EliminarCorazones();
                    corazones[i].visible = false;
                    vidas++;
                }
            }

            //Colisiones nitro
            for (int i = 0; i < nitro.Length; i++)
            {
                if (nitro[i].x == personaje.x && nitro[i].y == personaje.y && nitro[i].visible)
                {
                    nitro[i].visible = false;
                    personaje.velocidad=2;
                    estadoNitro = "ON";
                }
            }

            //Colisiones municion
            for (int i = 0; i < municion.Length; i++)
            {
                if (municion[i].x == personaje.x && municion[i].y == personaje.y && municion[i].visible)
                {
                    EliminarMunicion();
                    municion[i].visible = false;
                    balasPersonaje += 100;
                }
            }

            //Colisiones escudo
            if (escudo.x == personaje.x && escudo.y == personaje.y && escudo.visible)
            {
                EliminarEscudo();
                escudo.visible = false;
                estadoEscudo = "ON";
            }

            //Colisiones llave
            if (llaveMaestra.x == personaje.x && llaveMaestra.y == personaje.y)
            {
                EliminarLlave();
                llaveMaestra.visible = false;
                terminado = true;
            }
        }

        public bool ComprobarPartida()
        {
            if (vidas <= 0) terminado = true;
            else if (enemigosRestantes <= 0) terminado = true;
            return terminado;
        }

        public bool GameOver()
        {
            BorrarPantalla();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xCentroPantalla-2, yCentroPantalla);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(xCentroPantalla - 2, yCentroPantalla + 2);
            if ((vidas > 0 && enemigosRestantes<=0) || !llaveMaestra.visible) Console.WriteLine(" You won");
            else Console.WriteLine("You  lose");
            Thread.Sleep(3000);
            BorrarPantalla();
            Console.SetCursorPosition(xCentroPantalla - 2, yCentroPantalla);
            Console.WriteLine("PLAY AGAIN ?");
            Console.SetCursorPosition(xCentroPantalla - 12, yCentroPantalla+2);
            Console.WriteLine("·ESC to Exit  ·SPACEBAR to Restart");
            do
            {
                tecla = Console.ReadKey();

            } while (tecla.Key != ConsoleKey.Escape && tecla.Key != ConsoleKey.Spacebar);
            if (tecla.Key == ConsoleKey.Escape) return false; 
            else return true;
        }

        public void PausaHastaFinalDeFotograma()
        {
            Thread.Sleep(20);
        }
    }
}
