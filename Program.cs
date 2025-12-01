using System;
using System.Collections.Generic;
using System.IO;

namespace Zenelejatszo
{
    internal class Program
    {
        class Zene
        {
            public string Cim = "";
            public string Eloado = "";
            public string Mufaj = "";
            public double HosszPerc;

            public override string ToString()
            {
                return $"{Cim} - {Eloado} | {Mufaj} | {HosszPerc} perc";
            }
        }
        static void Beolvas(List<Zene> zenek)
        {
            string fajl = "C:\\szeva\\zene.txt";

            if (!File.Exists(fajl))
            {
                Console.WriteLine("A fájl nem található: " + fajl);
                Console.WriteLine("Üres lista indul.");
                Console.ReadKey();
                return;
            }

            using (StreamReader sr = new StreamReader(fajl))
            {
                sr.ReadLine(); // fejléc átugrása
                string? sor;
                while ((sor = sr.ReadLine()) != null)
                {
                    string[] adat = sor.Split(',');
                    if (adat.Length == 4)
                    {
                        Zene z = new Zene();
                        z.Cim = adat[0];
                        z.Eloado = adat[1];
                        z.Mufaj = adat[2];

                        double h;
                        if (double.TryParse(adat[3].Replace(",", "."), out h))
                            z.HosszPerc = h;

                        zenek.Add(z);
                    }
                }
            }
        }
        static void Mentes(List<Zene> zenek)
        {
            string fajl = "C:\\szeva\\zene.txt";

            Directory.CreateDirectory("C:\\szeva");

            using (StreamWriter sw = new StreamWriter(fajl))
            {
                foreach (Zene z in zenek)
                {
                    sw.WriteLine($"{z.Cim};{z.Eloado};{z.Mufaj};{z.HosszPerc}");
                }
            }
        }
        static void Main(string[] args)
        {
            List<Zene> zenek = new List<Zene>();
            Beolvas(zenek);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("0 - Kilépés");
                Console.WriteLine("1 - Zeneszámok betöltése listázása");
                Console.WriteLine("2 - Keresés cím szerint");
                Console.WriteLine("3 - Pop műfajú dalok megjelenítése");
                Console.WriteLine("4 - Legalább 3,5 perces zenék");
                Console.WriteLine("5 - Új zene hozzáadása");
                Console.WriteLine("6 - Zene törlése");
                Console.WriteLine("7 - Zene módosítása");

                string valasztas = Console.ReadLine() ?? "";
                int sorszam = 0;

                switch (valasztas)
                {
                    // Kilépés + mentés
                    case "0":
                        Mentes(zenek);
                        return;

                   
                    case "1":
                        Console.Clear();
                        sorszam = 0;
                        foreach (var z in zenek)
                            Console.WriteLine($"{sorszam++}. {z}");
                        break;

                  
                    case "2":
                        Console.Clear();
                        Console.Write("Add meg a keresett cím részletét: ");
                        string keres = (Console.ReadLine() ?? "").ToLower();

                        sorszam = 0;
                        foreach (var z in zenek)
                        {
                            if (z.Cim.ToLower().Contains(keres))
                                Console.WriteLine($"{sorszam++}. {z}");
                        }
                        break;

                    
                    case "3":
                        Console.Clear();
                        sorszam = 0;
                        foreach (var z in zenek)
                        {
                            if (z.Mufaj.ToLower() == "pop")
                                Console.WriteLine($"{sorszam++}. {z}");
                        }
                        break;

                   
                    case "4":
                        Console.Clear();
                        sorszam = 0;
                        foreach (var z in zenek)
                        {
                            if (z.HosszPerc >= 3.5)
                                Console.WriteLine($"{sorszam++}. {z}");
                        }
                        break;

                    
                    case "5":
                        Console.Clear();
                        Zene uj = new Zene();

                        Console.Write("Cím: ");
                        uj.Cim = Console.ReadLine() ?? "";

                        Console.Write("Előadó: ");
                        uj.Eloado = Console.ReadLine() ?? "";

                        Console.Write("Műfaj: ");
                        uj.Mufaj = Console.ReadLine() ?? "";

                        Console.Write("Hossz percben: ");
                        double hossz;
                        double.TryParse(Console.ReadLine() ?? "0", out hossz);
                        uj.HosszPerc = hossz;

                        zenek.Add(uj);
                        Console.WriteLine("Zene hozzáadva!");
                        break;

                 
        }
    }
}
