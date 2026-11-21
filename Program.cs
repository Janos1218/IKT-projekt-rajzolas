using System;
using System.IO;

namespace KonzolRajzolo
{
    
    class Program
    {
       
        const int MenuSzelesseg = 24;                
        const int VaszonSzelesseg = 60;               
        const int VaszonMagassag = 20;                
        const int VaszonX = MenuSzelesseg;            
        const int VaszonY = 1;                        
        const int UzenetSor = VaszonY + VaszonMagassag + 1;

       
        static char[,] vaszon = new char[VaszonSzelesseg, VaszonMagassag];
        static ConsoleColor[,] vaszonSzin = new ConsoleColor[VaszonSzelesseg, VaszonMagassag];

        static int x = VaszonSzelesseg / 2;          
        static int y = VaszonMagassag / 2;
        static bool leteve = false;                   
        static char rajzKarakter = '#';
        static bool fut = true;

       
        static readonly ConsoleColor[] szinek =
        {
            ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue,
            ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta
        };
        static readonly string[] szinNevek = { "fehér", "piros", "zöld", "kék", "sárga", "cián", "magenta" };
        static int szinIndex = 5; 

        static readonly char[] karakterek = { '#', '*', '+', '@', 'O', 'X', '░', '▓', '█' };
        static int karakterIndex = 0;

        
        static readonly string[] menuPontok =
        {
            "Karakter választása", "Szín választása", "Minta betöltése",
            "Cella törlése",       "Ugrás középre",   "Kilépés"
        };
        static int menuIndex = 0;
        static bool menuMod = false;     
        static bool listaAktiv = false;   
        static string listaCim = "";
        static string[] listaElemek = { };
        static int listaIndex = 0;

        
        static void Main()
        {
            try
            {
                Console.SetWindowSize(MenuSzelesseg + VaszonSzelesseg + 2, UzenetSor + 2);
                Console.SetBufferSize(MenuSzelesseg + VaszonSzelesseg + 2, UzenetSor + 2);
            }
            catch {  }
            Console.CursorVisible = false;
            VaszonTorol();

            Kirajzol();
            while (fut)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);
                Billentyu(k);
                if (fut) Kirajzol();
            }
            Console.ResetColor();
            Console.Clear();
        }

        
        static void Billentyu(ConsoleKeyInfo k)
        {
            if (listaAktiv) { ListaBillentyu(k); return; }

            if (menuMod) { MenuBillentyu(k); return; }

            switch (k.Key)
            {
                case ConsoleKey.LeftArrow:  Mozog(-1, 0); break;
                case ConsoleKey.RightArrow: Mozog(1, 0);  break;
                case ConsoleKey.UpArrow:    Mozog(0, -1); break;
                case ConsoleKey.DownArrow:  Mozog(0, 1);  break;

                case ConsoleKey.Spacebar:
                    leteve = !leteve;                 
                    if (leteve) Ir(x, y);             
                    break;

                case ConsoleKey.M:
                    menuMod = true;
                    break;

                default:
                    if (k.Key >= ConsoleKey.F1 && k.Key <= ConsoleKey.F12)
                    {
                        int n = (int)k.Key - (int)ConsoleKey.F1;   
                        int fajta = n / 3;                          
                        int sugar = n % 3 + 1;                      
                        Nyomda(fajta, sugar);
                    }
                    break;
            }
        }

        
        static void Mozog(int dx, int dy)
        {
            int ujX = x + dx, ujY = y + dy;
            if (ujX < 0 || ujX >= VaszonSzelesseg || ujY < 0 || ujY >= VaszonMagassag)
            {
                Console.Beep();                       
                return;
            }
            x = ujX; y = ujY;
            if (leteve) Ir(x, y);                     
        }

        static void Ir(int cx, int cy)
        {
            vaszon[cx, cy] = rajzKarakter;
            vaszonSzin[cx, cy] = szinek[szinIndex];
        }