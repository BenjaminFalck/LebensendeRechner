using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace LebensendeRechner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool bLäuft = true; // Bool-muuttuja tarkoitukseen [ while (running = true) ]
            string strAntwort = ""; //Später durch char ersetzen
            while (bLäuft)
            {
                Console.ResetColor();
                Lebensdauer();
                Console.WriteLine("Fragen wir noch einmal? J/N"); //Kysytäänkö uudelleen? Kyllä/Ei
                strAntwort = Console.ReadLine().ToUpper();
                switch (strAntwort)
                {
                    case "J": // Ja
                        Console.WriteLine("Roger roger... wir fragen noch einmal...");
                        break;
                    case "N": // Nein
                        bLäuft = false;
                        break;
                    default:
                        Console.WriteLine("Ungültige eingabe! ");
                        while (strAntwort != "J")
                        {
                            Console.WriteLine("Fragen wir noch einmal? J/N");
                            strAntwort = Console.ReadLine().ToUpper();
                        }
                        break;
                }


            }
        }

        private static void Lebensdauer()
        {
            double dVerbleibendeZeit = 0, dVerbleibendeJahre;
            int iLebenserwartung = 0, iÜberschritteneLebenserwartung = 0;
            string strGeburtsdatum = "", strGeschlecht = "", strJahreMonateTage = "", strFormat = "dd.MM.yyyy";
            DateTime dtGeburtsdatum, dtLebenserwartung, dtHeute = DateTime.Now;
            CultureInfo Kultur = CultureInfo.InvariantCulture;
            Console.WriteLine("Gib dein Geschleft an: (M=Mann/F=Frau)");
            strGeschlecht = Console.ReadLine().ToUpper();

            while (strGeschlecht != "M" && strGeschlecht != "F") // M = mies, F = Nainen
            {
                Console.WriteLine("Ungültiges Geschleht!\nGib dein Geschlecht erneut an: (M=Mann/F=Frau)");
                strGeschlecht = Console.ReadLine().ToUpper();
            }

            switch (strGeschlecht)
            {
                case "M": // Mann == Mies
                    iLebenserwartung = 78;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "F": // Frau == Nainen
                    iLebenserwartung = 84;
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;

                default:
                    iLebenserwartung = 0;
                    Console.WriteLine("Ungültige eingabe! "); //Virheellinen syöte!
                    break;
            }
            Console.WriteLine("Gib dein Geburtsdatum im Format TT.MM.JJJJ ein: "); // Anna syntymäaikasi muodossa PP.KK.VVVV

            strGeburtsdatum = Console.ReadLine();
            try
            {
                dtGeburtsdatum = DateTime.ParseExact(strGeburtsdatum, strFormat, Kultur);
                dtLebenserwartung = dtGeburtsdatum.AddYears(iLebenserwartung);
                dVerbleibendeZeit = dtLebenserwartung.Subtract(dtHeute).TotalDays;
                if (dVerbleibendeZeit < 0)
                {
                    iÜberschritteneLebenserwartung = (int)((dVerbleibendeZeit * -1) / 365.25);
                    dVerbleibendeZeit = 0;
                }
                dVerbleibendeJahre = dVerbleibendeZeit / 365.25;

                if (dVerbleibendeJahre > 20)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else if (dVerbleibendeJahre < 20 && dVerbleibendeJahre >= 2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else if (dVerbleibendeJahre < 2)
                {

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    SpieleTrauermusik();

                }


                DateTime dtTage = new DateTime(new TimeSpan((int)dVerbleibendeZeit, 0, 0, 0).Ticks);
                if (dVerbleibendeZeit == 0) Console.WriteLine($"Herzlichen Glückwunsch! Du hast länger gelebt als die durchschnittliche Lebenserwartung! Du hast die Erwartung um {iÜberschritteneLebenserwartung} Jahr(e) überschritten!"); //Onnea, olet ylittänyt odotteen x vuodella
                strJahreMonateTage = string.Format("{0} Jahre {1} Monate {2} Tage", dtTage.Year - 1, dtTage.Month - 1, dtTage.Day - 1);
            }
            catch (Exception ee)
            {
                Console.WriteLine("Das Program konnte der Datumsunterschied nicht berechen. Überprüfe das Datumsformat!");
                Console.WriteLine(ee.Message);
                dVerbleibendeZeit = 0;
            }
            if (dVerbleibendeZeit > 0) Console.WriteLine("Verbleibende Lebenserwartung: " + strJahreMonateTage + ".");
            Console.ResetColor(); // Korjaa (toivottavasti) bugin joka joskus for whatever reason avaa terminaalin väärän värisenä

        }

        private static void SpieleTrauermusik()
        {
            // Lyhyet nuotit toimii joskus ja joskus ei. (Eli joskus vain pitkät soi)
            // En ymmärrä miksi... Epäilen että liittyy koneen resursseihin.
            // Toimivielä ennen kunavasin toiselle näytölle kurssin videoa auki, mutta nyt enään ei toimi kuten pitäisi.
            Console.Beep(440, 600); // A
            Console.Beep(440, 600); // A
            Console.Beep(440, 600); // A
            Console.Beep(523, 450); // C
            Console.Beep(494, 100); // B
            Console.Beep(494, 450); // B
            Console.Beep(440, 100); // A
            Console.Beep(440, 450); // A
            Console.Beep(415, 100); // G#
            Console.Beep(440, 600); // A
        }
    }
}
