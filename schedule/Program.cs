Console.WriteLine("==== ROZVRH ====");
Console.WriteLine("1) Vytvořit nový rozvrh - volba tvorby rozvrhu.");
Console.WriteLine("2) Odstranit již vytvořený rozvrh - odstranění uloženého rozvrhu. (BONUSOVÁ ČÁST - NEPOVINNÉ)");
Console.WriteLine("3) Zobrazit již vytvořený rozvrh - zobrazení uloženého rozvrhu. (BONUSOVÁ ČÁST - NEPOVINNÉ)");
Console.WriteLine("4) Ukončit aplikaci - volba pro ukončení aplikace");

int choice;
while (!int.TryParse(Console.ReadLine(), out choice) || choice > 4 || choice < 0)
    Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");

static void CreateSchedule()
{
    List<string> subjects = new List<string>();
    List<string> subjectsIndexed = new List<string>();
    List<string> MonList = new List<string>();
    List<string> TueList = new List<string>();
    List<string> WenList = new List<string>();
    List<string> ThuList = new List<string>();
    List<string> FriList = new List<string>();

    String[] Days = { "PONDĚLÍ", "ÚTERÝ", "STŘEDA", "ČTVRTEK", "PÁTEK" };
    Console.WriteLine("==== VYTVOŘENÍ ROZVRHU ====");
    Console.Write("Zadejte svou třídu: ");
    string className = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(className)) // musi to vypnit
    {
        Console.WriteLine();
        Console.Write("Třída nemůže být prázdná. Zadejte prosím znovu: ");
        className = Console.ReadLine();
    }
    Console.Write("Zadejte počet předmetů, které budete chtít přidat: ");

    int subjectCount;

    while (!int.TryParse(Console.ReadLine(), out subjectCount) || subjectCount <= 0)
        Console.Write("Neplatné číslo, zadejte prosím znovu:");


    for (int a = 0; a < subjectCount; a++)
    {   
        Console.Write($"Zadejte název předmětu č. {a + 1}: ");
        string subjectName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(subjectName)) // musi to vypnit
        {
            Console.WriteLine();
            Console.Write($"Název předmětu č. {a + 1} nemůže být prázdný. Zadejte prosím znovu: ");
            subjectName = Console.ReadLine();
        }
        Console.Write($"Zadejte zkratku předmětu č. {a + 1} (jeden znak): ");   
        string SubjectShort = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(SubjectShort) || SubjectShort.Length > 1)
        {
            Console.WriteLine();
            Console.Write($"Zkratka předmětu č. {a + 1} musí být jeden znak. Zadejte prosím znovu: ");
            SubjectShort = Console.ReadLine();
        }
        subjects.Add(subjectName + " - " + "(" + SubjectShort + ")");
    }

    for (int day = 0; day < Days.Length; day++)
    {
        Console.WriteLine($"Zadejte počet hodin pro den {Days[day]} (1-8)");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count < 1 || count > 8)
        {
            Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
        }
        for (int i = 0; i < count; i++)
        {
            int index = 1;
            foreach (var subject in subjects)
            {
                Console.WriteLine(index + ") " + subject);
            }
            Console.WriteLine($"Zadejte předmět na den {Days[day]} {i + 1}. hodinu");
            int subjectChoice;
            while (!int.TryParse(Console.ReadLine(), out subjectChoice) || subjectChoice < 1 || subjectChoice > subjects.Count)
            {
                Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
            }
            switch (day)
            {
                case 0:
                    MonList.Add(subjects[subjectChoice - 1]);
                    break;
                case 1:
                    //UteList.Add(subjects[subjectChoice - 1]);
                    break;
                case 2:
                    //StreList.Add(subjects[subjectChoice - 1]);
                    break;
                case 3:
                    //CtvList.Add(subjects[subjectChoice - 1]);
                    break;
                case 4:
                    //PatList.Add(subjects[subjectChoice - 1]);
                    break;
            }
            MonList.Add(subjects[subjectChoice - 1]);
        }
    }
    
}

switch (choice)
{
    case 1:
        CreateSchedule();
        break;
    case 2:
        //DeleteSchedule();
        break;
    case 3:
        //ViewSchedule();
        break;
    case 4:
        return;
    default:
        Console.WriteLine("Neplatná volba.");
        break;
}