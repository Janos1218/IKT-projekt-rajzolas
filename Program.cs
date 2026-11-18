using System;
using System.IO;

namespace KonzolRajzolo
{
    // ============================================================
    //  KONZOL RAJZOLÓ – IKT projekt (11. évfolyam)
    //  Egyfájlos megoldás: menu + állapotpanel (bal), rajzlap (jobb)
    //  Vezérlés: nyilak = mozgás, szóköz = eszköz, F1–F12 = nyomda,
    //            M = menü, Esc = vissza a rajzhoz
    // ============================================================
    class Program
    {
        // ---------- Képernyő-elrendezés ----------
        const int MenuSzelesseg = 24;                 
        const int VaszonSzelesseg = 60;               
        const int VaszonMagassag = 20;                
        const int VaszonX = MenuSzelesseg;            
        const int VaszonY = 1;                        
        const int UzenetSor = VaszonY + VaszonMagassag + 1;

        // ---------- Rajzlap állapota ----------
        static char[,] vaszon = new char[VaszonSzelesseg, VaszonMagassag];
        static ConsoleColor[,] vaszonSzin = new ConsoleColor[VaszonSzelesseg, VaszonMagassag];

        static int x = VaszonSzelesseg / 2;           
        static int y = VaszonMagassag / 2;
        static bool leteve = false;                   
        static char rajzKarakter = '#';
        static bool fut = true;

        // ---------- Paletták ----------
        static readonly ConsoleColor[] szinek =
        {
            ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue,
            ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta
        };
        static readonly string[] szinNevek = { "fehér", "piros", "zöld", "kék", "sárga", "cián", "magenta" };
        static int szinIndex = 5; 

        static readonly char[] karakterek = { '#', '*', '+', '@', 'O', 'X', '░', '▓', '█' };
        static int karakterIndex = 0;

        // ---------- Menü / választólista állapota ----------
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

        // ============================================================
        static void Main()
        {
            try
            {
                Console.SetWindowSize(MenuSzelesseg + VaszonSzelesseg + 2, UzenetSor + 2);
                Console.SetBufferSize(MenuSzelesseg + VaszonSzelesseg + 2, UzenetSor + 2);
            }
            catch {}
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

       