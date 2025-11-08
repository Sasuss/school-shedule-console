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
    List<string> MonList = new List<string>();
    List<string> TueList = new List<string>();
    List<string> WenList = new List<string>();
    List<string> ThuList = new List<string>();
    List<string> FriList = new List<string>();
    String[] Days = { "PONDĚLÍ", "ÚTERÝ", "STŘEDA", "ČTVRTEK", "PÁTEK" };


    Console.WriteLine("==== VYTVOŘENÍ ROZVRHU ====");


    /// CREATE CLASS
    Console.Write("Zadejte svou třídu: ");

    string className = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(className)) 
    {
        Console.WriteLine();
        Console.Write("Třída nemůže být prázdná. Zadejte prosím znovu: ");
        className = Console.ReadLine();
    }
    
    Console.Write("Zadejte počet předmetů, které budete chtít přidat: ");
    int subjectCount;
    while (!int.TryParse(Console.ReadLine(), out subjectCount) || subjectCount <= 0)
        Console.Write("Neplatné číslo, zadejte prosím znovu:");


    /// ADD SUBJECTS
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

    /// ASSIGN SUBJECTS TO DAYS
    for (int day = 0; day < Days.Length; day++)
    {
        Console.WriteLine($"Zadejte počet hodin pro den {Days[day]} (1-8)");
        int SubjectsForDay;
        while (!int.TryParse(Console.ReadLine(), out SubjectsForDay) || SubjectsForDay < 1 || SubjectsForDay > 8)
        {
            Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
        }
        /// CHOOSE SUBJECTS FOR DAY
        for (int i = 0; i < SubjectsForDay; i++)
        {
            int index = 1;
            foreach (var subject in subjects) /// DISPLAY SUBJECTS
            {
                Console.WriteLine(index + ") " + subject);
                index++;
            }
            Console.WriteLine($"Zadejte předmět na den {Days[day]} {i + 1}. hodinu");
            int subjectChoice;
            while (!int.TryParse(Console.ReadLine(), out subjectChoice) || subjectChoice < 1 || subjectChoice > subjects.Count)
            {
                Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
            }
            /// ASIGN SUBJECT TO DAY LIST
            switch (day)
            {
                case 0:
                    MonList.Add(subjects[subjectChoice - 1]);
                    break;
                case 1:
                    TueList.Add(subjects[subjectChoice - 1]);
                    break;
                case 2:
                    WenList.Add(subjects[subjectChoice - 1]);
                    break;
                case 3:
                    ThuList.Add(subjects[subjectChoice - 1]);
                    break;
                case 4:
                    FriList.Add(subjects[subjectChoice - 1]);
                    break;
            }
            Console.WriteLine("MonList " + string.Join(", ", MonList));
            Console.WriteLine("subjects " + string.Join(", ", subjects));


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