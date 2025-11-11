static (List<string[]> subjects, List<string[]>[] SubjectsByDays, string className) CreateSchedule()
{
    List<string[]> subjects = new List<string[]>();
    string[] Days = { "PONDĚLÍ", "ÚTERÝ", "STŘEDA", "ČTVRTEK", "PÁTEK" };

    List<string[]>[] SubjectsByDays = new List<string[]>[Days.Length];
    for (int i = 0; i < Days.Length; i++)
        SubjectsByDays[i] = new List<string[]>();

    Console.WriteLine("==== VYTVOŘENÍ ROZVRHU ====");

    /// CREATE CLASS
    Console.Write("Zadejte svou třídu: ");
    string className = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(className))
    {
        Console.Write("Třída nemůže být prázdná. Zadejte prosím znovu: ");
        className = Console.ReadLine();
    }

    Console.Write("Zadejte počet předmětů, které budete chtít přidat: ");
    int subjectCount;
    while (!int.TryParse(Console.ReadLine(), out subjectCount) || subjectCount <= 0)
        Console.Write("Neplatné číslo, zadejte prosím znovu: ");

    /// ADD SUBJECTS
    for (int a = 0; a < subjectCount; a++)
    {
        string[] SubjData = new string[3]; // 0 = subj name, 1 = short name, 2 = print format

        Console.Write($"Zadejte název předmětu č. {a + 1}: ");
        string subjectName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(subjectName))
        {
            Console.Write($"Název předmětu č. {a + 1} nemůže být prázdný. Zadejte prosím znovu: ");
            subjectName = Console.ReadLine();
        }

        Console.Write($"Zadejte zkratku předmětu č. {a + 1} (jeden znak): ");
        string SubjectShort = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(SubjectShort) || SubjectShort.Length > 1)
        {
            Console.Write($"Zkratka předmětu č. {a + 1} musí být jeden znak. Zadejte prosím znovu: ");
            SubjectShort = Console.ReadLine();
        }

        SubjData[0] = subjectName;
        SubjData[1] = SubjectShort;
        SubjData[2] = $"{subjectName} ({SubjectShort})";
        subjects.Add(SubjData);
    }

    /// ASSIGN SUBJECTS TO DAYS
    for (int day = 0; day < Days.Length; day++)
    {
        Console.WriteLine($"\nZadejte počet hodin pro den {Days[day]} (1-8): ");
        int SubjectsForDay;
        while (!int.TryParse(Console.ReadLine(), out SubjectsForDay) || SubjectsForDay < 1 || SubjectsForDay > 8)
        {
            Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
        }

        for (int i = 0; i < SubjectsForDay; i++)
        {
            Console.WriteLine($"\nVýběr pro {Days[day]} {i + 1}. hodinu:");
            for (int j = 0; j < subjects.Count; j++)
                Console.WriteLine($"{j + 1}) {subjects[j][2]}");

            int subjectChoice;
            while (!int.TryParse(Console.ReadLine(), out subjectChoice) || subjectChoice < 1 || subjectChoice > subjects.Count)
            {
                Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
            }

            SubjectsByDays[day].Add(subjects[subjectChoice - 1]);
        }
    }

    return (subjects, SubjectsByDays, className);
}

static void EditSchedule(List<string[]> subjects, List<string[]>[] SubjectsByDays)
{
    Console.WriteLine("==========================");
    Console.WriteLine("==== UPRAVENÍ ROZVRHU ====");

    Console.WriteLine("Zadejte den změny předmetu:");
    for (int i = 0; i < SubjectsByDays.Length; i++)
    { // dodelat ne switch
        string dayName = i switch
        {
            0 => "PONDĚLÍ",
            1 => "ÚTERÝ",
            2 => "STŘEDA",
            3 => "ČTVRTEK",
            4 => "PÁTEK",
            _ => ""
        };
        Console.WriteLine($"{i + 1}) {dayName}");
        int day;
        while (!int.TryParse(Console.ReadLine(), out day) || i < 1 || i > SubjectsByDays.Length)
        {
            Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
        }



        Console.WriteLine("Kolikátá hodina má být v rozvrhu změněna?");
        int hour;
        while (!int.TryParse(Console.ReadLine(), out hour) || hour < 1 || hour > SubjectsByDays[i].Count)
        {
            Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
        }

        Console.WriteLine("Na této pozici se nachází předmět: " + SubjectsByDays[day][1]);

        Console.WriteLine("OZNAČENÍ PŘEDMĚTŮ:");

        for (int j = 0; j < subjects.Count; j++)
            Console.WriteLine($"{j + 1}) {subjects[j][2]}");
        Console.WriteLine("Jaký předmět chcete nově zadat?");
        int newSubject;
        while (!int.TryParse(Console.ReadLine(), out newSubject) || newSubject < 1 || newSubject > subjects.Count)
        {
            Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
        }

        SubjectsByDays[day][hour] = subjects[newSubject - 1];
    }
}
void ViewSchedule(List<string[]>[] SubjectsByDays, string className)
{
    Console.WriteLine("=========================");
    Console.WriteLine("==== AKTUÁLNÍ ROZVRH ====");

    Console.WriteLine("Třída: " + className);

    Console.WriteLine("   |1||2||3||4||5||6||7||8|");
    Console.WriteLine("===========================");
    for (int i = 0; i < SubjectsByDays.Length; i++)
    {
        string dayName = i switch
        {
            0 => "Po",
            1 => "Út",
            2 => "St",
            3 => "Čt",
            4 => "Pá",
            _ => ""
        };
        Console.Write(dayName + "|");
        for (int j = 0; j < 8; j++)
        {
            if (j < SubjectsByDays[i].Count)
                Console.Write("|" + SubjectsByDays[i][j][1] + "|");
            else
            {
                break;
            }
                
        }
        Console.WriteLine();
        Console.WriteLine("---------------------------");
    }
    

}

void ShowSubjects(List<string[]> subjects)
{
    Console.WriteLine("==== OZNAČENÍ PŘEDMĚTŮ ====");
    for (int i = 0; i < subjects.Count; i++)
    {
        Console.WriteLine($"{subjects[i][2]}");
    }
}

void MainMenu(List<string[]> subjects, List<string[]>[] SubjectsByDays, string className)
{
    Console.WriteLine("==============");
    Console.WriteLine("==== MENU ====");
    Console.WriteLine("1) Upravení aktualního rozvrhu");
    Console.WriteLine("2) Zobrazení aktualního rozvrhu");
    Console.WriteLine("3) Zobrazit označení používaných předmetů");
    Console.WriteLine("4) Zobrazit rozvrh pro vybraný pracovní den");
    Console.WriteLine("5) Určení předmětu");
    Console.WriteLine("6) Ukončení aplikace");
    Console.WriteLine("7) Přidat nebo odebrat předmět");
    Console.WriteLine("8) Změnit označení třídy");
    Console.WriteLine("9) Změnit počet hodin");
    Console.WriteLine("10) Hledání předmětu");
    Console.WriteLine("11) Uložit rozvrh");
    Console.WriteLine("12) Vrátit se do hlavního menu");
    Console.WriteLine("=================");

    int choice;
    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 12)
    {
        Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
    }

    switch (choice)
    {
        case 1:
            EditSchedule(subjects, SubjectsByDays);
            break;
        case 2:
            ViewSchedule(SubjectsByDays, className);
            break;
        case 3:
            //ShowSubjects();
            break;
        case 4:
            //ViewScheduleByDay();
            break;
        case 5:
            //LocateSubject();
            break;
        case 6:
            return;
        case 7:
            //AddOrRemoveSubject();
            break;
        case 8:
            //ChangeClassName();
            break;
        case 9:
            //ChangeNumberOfHours();
            break;
        case 10:
            //SearchSubject();
            break;
        case 11:
            //SaveSchedule();
            break;
        case 12:
            //MainMenu();
            break;
        default:
            Console.WriteLine("Neplatná volba.");
            break;
    }

    MainMenu(subjects, SubjectsByDays, className);

}

Console.WriteLine("==== ROZVRH ====");
Console.WriteLine("1) Vytvořit nový rozvrh - volba tvorby rozvrhu.");
Console.WriteLine("2) Odstranit již vytvořený rozvrh - odstranění uloženého rozvrhu. (BONUSOVÁ ČÁST - NEPOVINNÉ)");
Console.WriteLine("3) Zobrazit již vytvořený rozvrh - zobrazení uloženého rozvrhu. (BONUSOVÁ ČÁST - NEPOVINNÉ)");
Console.WriteLine("4) Ukončit aplikaci - volba pro ukončení aplikace");

int choice;
while (!int.TryParse(Console.ReadLine(), out choice) || choice > 4 || choice < 0)
    Console.WriteLine("Neplatné číslo, zadejte prosím znovu:");
switch (choice)
{
    case 1:
        var (subjects, SubjectsByDays, className) = CreateSchedule(); // ai helped me with this (i had no idea how tuples in c# worked)
        ViewSchedule(SubjectsByDays, className);
        MainMenu(subjects, SubjectsByDays, className);
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






