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

       
        static void Nyomda(int fajta, int sugar)
        {
           
            for (int dy = -sugar; dy <= sugar; dy++)
                for (int dx = -sugar; dx <= sugar; dx++)
                    if (NyomdaCella(fajta, dx, dy, sugar))
                    {
                        int cx = x + dx, cy = y + dy;
                        if (cx < 0 || cx >= VaszonSzelesseg || cy < 0 || cy >= VaszonMagassag)
                        {
                            Console.Beep();
                            Uzenet("A nyomda kilóg a rajzlapról!");
                            return;
                        }
                    }

           
            for (int dy = -sugar; dy <= sugar; dy++)
                for (int dx = -sugar; dx <= sugar; dx++)
                    if (NyomdaCella(fajta, dx, dy, sugar))
                        Ir(x + dx, y + dy);
           
        }

        static bool NyomdaCella(int fajta, int dx, int dy, int sugar)
        {
            switch (fajta)
            {
                case 0: return Math.Abs(dx) == Math.Abs(dy);                
                case 1: return dx == 0 || dy == 0;                          
                case 2: return Math.Max(Math.Abs(dx), Math.Abs(dy)) == sugar;
                case 3: return Math.Abs(dx) + Math.Abs(dy) == sugar;        
                default: return false;
            }
        }

       
        static void MenuBillentyu(ConsoleKeyInfo k)
        {
            switch (k.Key)
            {
                case ConsoleKey.UpArrow:   menuIndex = (menuIndex + menuPontok.Length - 1) % menuPontok.Length; break;
                case ConsoleKey.DownArrow: menuIndex = (menuIndex + 1) % menuPontok.Length; break;
                case ConsoleKey.Escape:    menuMod = false; break;
                case ConsoleKey.Enter:     MenuVegrehajt(); break;
            }
        }

        static void MenuVegrehajt()
        {
            switch (menuIndex)
            {
                case 0: ListaIndit("Karakter választása", Array.ConvertAll(karakterek, c => c.ToString()), karakterIndex, i => karakterIndex = i); break;
                case 1: ListaIndit("Szín választása", szinNevek, szinIndex, i => szinIndex = i); break;
                case 2: MintaBetoltese(); break;
                case 3: CellaTorlese(); break;          
                case 4: UgrasKozepre(); break;          
                case 5: fut = false; break;
            }
        }

       
        static Action<int> listaValaszt;

        static void ListaIndit(string cim, string[] elemek, int kezdo, Action<int> eredmeny)
        {
            listaCim = cim;
            listaElemek = elemek;
            listaIndex = kezdo;
            listaValaszt = eredmeny;
            listaAktiv = true;
        }

        static void ListaBillentyu(ConsoleKeyInfo k)
        {
            switch (k.Key)
            {
                case ConsoleKey.UpArrow:   listaIndex = (listaIndex + listaElemek.Length - 1) % listaElemek.Length; break;
                case ConsoleKey.DownArrow: listaIndex = (listaIndex + 1) % listaElemek.Length; break;
                case ConsoleKey.Escape:    listaAktiv = false; break;
                case ConsoleKey.Enter:
                    listaValaszt(listaIndex);
                    listaAktiv = false;
                    break;
            }
        }

       
        static void CellaTorlese()
        {
            vaszon[x, y] = ' ';
            Uzenet("Cella törölve.");
        }

       
        static void UgrasKozepre()
        {
            x = VaszonSzelesseg / 2;
            y = VaszonMagassag / 2;
            Uzenet("Kurzor a rajzlap közepén.");
        }

       
        static void MintaBetoltese()
        {
            bool ujra = true;
            while (ujra && fut)
            {
                Kirajzol();
                Console.SetCursorPosition(0, UzenetSor);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Minta fájl neve: ");
                Console.CursorVisible = true;
                string nev = Console.ReadLine() ?? "";
                Console.CursorVisible = false;

                if (nev.Trim() == "") { menuMod = false; return; }  

                try
                {
                   
                    string[] sorok = File.ReadAllLines(nev);
                    if (sorok.Length > VaszonMagassag)
                        throw new Exception($"Túl sok sor (max. {VaszonMagassag}).");
                    foreach (string s in sorok)
                        if (s.Length > VaszonSzelesseg)
                            throw new Exception($"Túl hosszú sor (max. {VaszonSzelesseg} karakter).");

                    VaszonTorol();
                    for (int j = 0; j < sorok.Length; j++)
                        for (int i = 0; i < sorok[j].Length; i++)
                        {
                            vaszon[i, j] = sorok[j][i];
                            vaszonSzin[i, j] = szinek[szinIndex];  
                        }
                    Uzenet("Minta betöltve: " + nev);
                    ujra = false;
                }
                catch (Exception ex)
                {
                    Console.Beep();                                  
                    Kirajzol();
                    Console.SetCursorPosition(0, UzenetSor);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Hiba: " + ex.Message + "  Újra? (i/n): ");
                    ConsoleKeyInfo v = Console.ReadKey(true);
                    ujra = (v.KeyChar == 'i' || v.KeyChar == 'I');
                }
            }
        }

       
        static void VaszonTorol()
        {
            for (int j = 0; j < VaszonMagassag; j++)
                for (int i = 0; i < VaszonSzelesseg; i++)
                {
                    vaszon[i, j] = ' ';
                    vaszonSzin[i, j] = ConsoleColor.Gray;
                }
        }

       
        static void Kirajzol()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 0); Console.Write("KONZOL RAJZOLÓ");
            Console.SetCursorPosition(VaszonX, 0); Console.Write("| RAJZLAP");
            Console.ForegroundColor = ConsoleColor.Gray;

           
            if (listaAktiv)
            {
                Console.SetCursorPosition(2, 2); Console.Write(listaCim);
                for (int i = 0; i < listaElemek.Length; i++)
                {
                    Console.SetCursorPosition(2, 4 + i);
                    Console.ForegroundColor = (i == listaIndex) ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write((i == listaIndex ? "> " : "  ") + listaElemek[i]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, UzenetSor);
                Console.Write("Fel/Le: választás  Enter: kész  Esc: vissza");
            }
            else if (menuMod)
            {
                for (int i = 0; i < menuPontok.Length; i++)
                {
                    Console.SetCursorPosition(2, 2 + i);
                    Console.ForegroundColor = (i == menuIndex) ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write((i == menuIndex ? "> " : "  ") + menuPontok[i]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, UzenetSor);
                Console.Write("Fel/Le: választás  Enter: megnyitás  Esc: vissza a rajzhoz");
            }
            else
            {
                Console.SetCursorPosition(0, 2); Console.Write("M   Menü");
                Console.SetCursorPosition(0, 3); Console.Write("X   Kilépés");
                Console.SetCursorPosition(0, 9);  Console.Write($"Pozíció ({x}, {y})");
                Console.SetCursorPosition(0, 10); Console.Write("Karakter " + rajzKarakter);
                Console.SetCursorPosition(0, 11); Console.Write("Szín     " + szinNevek[szinIndex]);
                Console.ForegroundColor = szinek[szinIndex];
                Console.SetCursorPosition(15, 11); Console.Write("■■■");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, 12); Console.Write("Eszköz   " + (leteve ? "letéve" : "felemelve"));
                Console.SetCursorPosition(0, UzenetSor);
                Console.Write("Nyilak: mozgás  Szóköz: eszköz  F1–F12: nyomda  M: menü");
            }

           
            for (int j = 0; j < VaszonMagassag; j++)
                for (int i = 0; i < VaszonSzelesseg; i++)
                {
                    Console.SetCursorPosition(VaszonX + i, VaszonY + j);
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = leteve ? szinek[szinIndex] : ConsoleColor.White;
                        Console.Write(leteve ? rajzKarakter : '▉');  
                    }
                    else
                    {
                        Console.ForegroundColor = vaszonSzin[i, j];
                        Console.Write(vaszon[i, j]);
                    }
                }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, UzenetSor + 1);
        }

        static void Uzenet(string szoveg)
        {
            Console.SetCursorPosition(0, UzenetSor + 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(szoveg.PadRight(MenuSzelesseg + VaszonSzelesseg));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
