namespace Bloggen
{
    class Program
    {
        static void Main(string[] args) // Main Metoden.
        {
            bool Sortering = false;
            bool Körtid = true;   // Här skapar jag meny loopen.        
            List<string[]> Inlägg = new List<string[]>(); // Listan där alla bloggar kommer att sparas.
            while (Körtid)
            {
                Console.ForegroundColor = ConsoleColor.White;  // Använder färger för det var roligt att leka med :).
                Console.Clear();
                Console.Write("Välkommen till bloggen!\n" +     // Här startas själva menyn. Detta är vad användaren kommer att se när programmet startar. Jag använder \n för att undvika upprepande kod.
                    "[1] Skapa ett nytt inlägg\n" +
                    "[2] Skriv ut alla inlägg\n" +
                    "[3] Sök efter inlägg\n" +
                    "[4] Avsluta Bloggen\n" +
                    "[5] Sortera alla inlägg\n" +
                    "[6] Radera ett inlägg\n" +
                    "\nVar god och skriv en siffra: ");
                if (Int32.TryParse(Console.ReadLine(), out int Meny)) // Ser till att inga körtids fel inträffar.
                {
                    switch (Meny)
                    {
                        case 1:                                // ### CASE 1 ### 
                            DateTime Datum = DateTime.Now;    // Skapar DateTime Datum som kommer att spara det aktiva datumet.
                            string[] Blogg = new string[3];  // Skapar 3 element som kommer att användas till bloggen. Titel, Inlägg och Datum.
                            Console.Clear();
                            Console.Write("Skriv din titel: ");  // Användaren skriver in sin titel.
                            Blogg[0] = Console.ReadLine();   // Sparar användarens titel i element 0.                                                                                                                   
                            Console.Clear();
                            Console.Write("Skriv inlägg: ");  // Användaren skrier in sitt inlägg.
                            Blogg[1] = Console.ReadLine();   // Sparar användarens inlägg i element 1.                                                     
                            Blogg[2] = Datum.ToString();     // Sparar aktiva datumet i element 2. Jag använder ToString() för att konvertera det till en string.                 
                            Inlägg.Add(Blogg); // Alla element läggs in till listan "Inlägg" där nu en blogg har skapats.
                            BloggUtskrift(Inlägg); // Kallar på BloggUtskrift metoden så användaren får se vad han/hon har skrivit in.
                            Tillbaka(); // Kallar på Tillbaka metoden.
                            break;

                        case 2:                            // ### CASE 2 ### 
                            if (Inlägg.Count == 0) // Här kollar konsolen om användaren har gjort ett inlägg.
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Du har inte gjort något inlägg.\n"); // Om användaren inte har gjort något inlägg så kommer konsolen berätta det.
                            }
                            else // Om det finns inlägg så händer det under.                            
                                BloggUtskrift(Inlägg); // Kallar på BloggUtskrift metoden som kommar att skriva ut alla inlägg användaren har gjort.
                            Tillbaka(); // Kallar på Tillbaka metoden som kommer att be användaren trycka på enter.
                            break;

                        case 3: // Starta sökning                     ### CASE 3 ### Innehåller binär sökning.
                            if (Sortering == true)
                            {
                                Console.Clear();
                                Console.Write("Ange titel på ditt inlägg: "); // Användaren anger sitt titel.
                                string key = Console.ReadLine(); // Sparar sökningen.
                                int Första = 0;
                                int Sista = Inlägg.Count - 1;
                                while (Första <= Sista)
                                {
                                    int Mellan = (Första + Sista) / 2; // Första = 0, sista = antal inlägg och sedan delat på 2.
                                    int Compare = key.CompareTo(Inlägg[Mellan][0]); // CompareTo mellan sökningen och inläggen.
                                    if (Compare > 0) // Om "Compare" är större än 0 så fortsätter den leta.
                                        Första = Mellan + 1;
                                    else if (Compare < 0)
                                        Sista = Mellan - 1;
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Hittade:\n " // Presentera inlägget.
                                            + "\nTitel: " + Inlägg[Mellan][0]
                                            + "\nInlägg: " + Inlägg[Mellan][1]
                                            + "\nInlagd: " + Inlägg[Mellan][2] + "\n");
                                        Tillbaka();
                                        break;
                                    }
                                }
                                if (Första > Sista)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine(key + " Finnns inte i bloggen!\n");
                                    Tillbaka();
                                }
                            }
                            else if (Sortering == false)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Du måste sortera dina inlägg innan du kan söka.\n");
                                Tillbaka();
                            }
                            break;
                        case 6:       // case 6 innehåller min gamla linjära sökning men jag tycker att den funkade bra så jag använder den för att rensa inlägg.
                            if (Inlägg.Count == 0)  // OM antal bloggar är lika med 0.
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Clear();
                                Console.WriteLine("Du måste ha något inlägg för att kunna söka.\n"); // Berätta för användaren att han/hon behöver göra ett inlägg.
                                Tillbaka();
                            }
                            else // ANNARS 
                            {
                                Console.Clear();
                                Console.Write("Ange titel på ditt inlägg: "); // Be användaren skriva in namnet på sitt inlägg.
                                string Sök = Console.ReadLine();
                                bool sökBool = false;
                                for (int i = 0; i < Inlägg.Count; i++)
                                {
                                    if (Inlägg[i][0].ToUpper() == Sök.ToUpper()) // Om blogg namn matchar användarens söking.
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Hittade:\n\n" +  // Printa ut användarens blogg.
                                            "Titel: " + Inlägg[i][0] +
                                            "\nInlägg: " + Inlägg[i][1] +
                                            "\nDatum: " + Inlägg[i][2] + "\n");
                                        sökBool = true;

                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write("Skriv [1] om du vill ta bort denna blogg. Annars tryck på enter: "); // Denna del ingår inte i psuedokoden utan detta är ännu ett val för användaren att kunna ta bort sitt inlägg.

                                        Int32.TryParse(Console.ReadLine(), out int Bort); // För att inte programmet ska krasha.
                                        if (Bort == 1) // Om "Bort" är lika med 1 så kommer bloggen att rensas.
                                            Inlägg.RemoveAt(i);
                                    }

                                }
                                if (sökBool != true) // OM "sökBool" inte blir sann.                      
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Clear();
                                    Console.WriteLine("Hittade inget med sök resultatet: " + Sök + "\n"); // Berätta för användaren att sökresultatet inte finns.
                                    Tillbaka();
                                }
                            }
                            break;
                        case 4:              // ### CASE 4 ###
                            Körtid = false; // Körtids loopen som för användaren tillbaka till menyn ändras till false, vilket gör att programmet avslutas.
                            break;
                        case 5: // ### CASE 5 ### Innehåller min bubbelsortering.
                            if (Inlägg.Count > 0)
                            {
                                int Sort = Inlägg.Count - 1;
                                for (int i = 0; i < Sort; i++)
                                {
                                    int Vänster = Sort - i;
                                    for (int h = 0; h < Vänster; h++)
                                    {
                                        int tmp = Inlägg[h][0].CompareTo(Inlägg[h + 1][0]);
                                        if (tmp > 0)
                                        {
                                            string[] Byt = Inlägg[h];
                                            Inlägg[h] = Inlägg[h + 1];
                                            Inlägg[h + 1] = Byt;
                                        }
                                    }
                                }
                                Sortering = true;
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Nu har dina inlägg sorterats.\n");
                                Tillbaka();
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Det finns inget att sortera.\n");
                                Tillbaka();
                            }
                            break;
                    }
                }
                else // Den här ingår i TryParsen längre upp. Om användaren gjort en felaktig inmatning så kommer konsolen berätta det.
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Du måste ange en siffra!\n");
                    Tillbaka(); // Tillbaka metoden.
                }
            }
        }
        static void BloggUtskrift(List<string[]> Inlägg)  // Här skapar jag en metod som skriver ut alla inlägg, detta gör jag För att undvika upprepande kod.
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Alla dina inlägg: \n");
            foreach (string[] Bloggar in Inlägg) // Använder foreach för att spara alla inlägg i Bloggar där jag senare kan skriva ut alla element.
            {
                Console.WriteLine("Titel: " + Bloggar[0]
                    + "\nInlägg: " + Bloggar[1]
                    + "\nInlagd: " + Bloggar[2] + "\n");
            }
        }
        static void Tillbaka() // Tillbaka metoden används också för att undvika upprepande kod.
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Tryck på enter för att gå tillbaka!"); // Användaren trycker på enter för att komma tillbaka till menyn.
            Console.ReadLine();
        }
    }
}
