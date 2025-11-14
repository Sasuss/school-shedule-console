using System.IO;
using System.Text.Json;


const int NAME = 0;
const int SHORT = 1;
const int DISPLAY = 2;

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

        SubjData[NAME] = subjectName;
        SubjData[SHORT] = SubjectShort;
        SubjData[DISPLAY] = $"{subjectName} ({SubjectShort})";
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

static int GetIntInput(string text, int from = 0, int to = 0)
{
    int number;

    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out number))
        {
            if (from == 0 && to == 0)
                return number;

            if (number >= from && number <= to)
                return number;
        }

        Console.WriteLine(text);
    }
}
static void EditSchedule(List<string[]> subjects, List<string[]>[] SubjectsByDays)
{
    Console.WriteLine("==========================");
    Console.WriteLine("==== UPRAVENÍ ROZVRHU ====");

    Console.WriteLine("Zadejte den změny předmetu:");


    Console.WriteLine("1) Pondělí");
    Console.WriteLine("2) Úterý");
    Console.WriteLine("3) Středa");
    Console.WriteLine("4) Čtvrtek");
    Console.WriteLine("5) Pátek");

    var day = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, SubjectsByDays.Length);



    Console.WriteLine("Kolikátá hodina má být v rozvrhu změněna?");
    var hour = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, SubjectsByDays[day - 1].Count);
    try
    {
        Console.WriteLine("Na této pozici se nachází předmět: " + SubjectsByDays[day-1][hour-1][SHORT]);
    } catch
    { 
        Console.WriteLine("Na této pozici se nenachází žádný předmět.");
        Console.WriteLine("day: " + day);
        return;
    }


        Console.WriteLine("OZNAČENÍ PŘEDMĚTŮ:");

        for (int j = 0; j < subjects.Count; j++)
            Console.WriteLine($"{j + 1}) {subjects[j][2]}");
        Console.WriteLine("Jaký předmět chcete nově zadat?");
        var newSubject = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, subjects.Count);


        SubjectsByDays[day - 1][hour - 1] = subjects[newSubject - 1];
        Console.WriteLine("Předmět byl úspěšně změněn.");
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
                Console.Write("|" + SubjectsByDays[i][j][SHORT] + "|");
            else
            {
                break;
            }
                
        }
        Console.WriteLine();
        Console.WriteLine("---------------------------");
    }
    

}

void PrintDays()
{
    Console.WriteLine("1) Po");
    Console.WriteLine("2) Út");
    Console.WriteLine("3) St");
    Console.WriteLine("4) Čt");
    Console.WriteLine("5) Pá");
}

void ShowSubjects(List<string[]> subjects)
{
    Console.WriteLine("==== OZNAČENÍ PŘEDMĚTŮ ====");
    for (int i = 0; i < subjects.Count; i++)
    {
        Console.WriteLine($"{subjects[i][2]}");
    }
}

void ViewScheduleByDay(List<string[]>[] SubjectsByDays)
{
    Console.WriteLine("==============================");
    Console.WriteLine("==== VYBRANÝ PRACOVNÍ DEN ====");
    Console.WriteLine("Zadejte den, pro který chcete zobrazit rozvrh:");
    PrintDays();

    var day = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 5);


    Console.WriteLine("==========================");
    string shortDay = day switch
    {
        1 => "Po",
        2 => "Út",
        3 => "St",
        4 => "Čt",
        5 => "Pá",
        _ => ""
    };
    switch (day)
    {
        case 1:
            Console.WriteLine("Rozvrh pro Pondělí:");
            break;
        case 2:
            Console.WriteLine("Rozvrh pro Úterý:");
            break;
        case 3:
            Console.WriteLine("Rozvrh pro Středu:");
            break;
        case 4:
            Console.WriteLine("Rozvrh pro Čtvrtek:");
            break;
        case 5:
            Console.WriteLine("Rozvrh pro Pátek:");
            break;
    }

    Console.WriteLine("   |1||2||3||4||5||6||7||8|");
    Console.WriteLine("===========================");
    Console.Write(shortDay + "|");
    for (int j = 0; j < 8; j++)
    {
        if (j < SubjectsByDays[day-1].Count)
            Console.Write("|" + SubjectsByDays[day-1][j][SHORT] + "|");
        else
        {
            break;
        }

    }
    Console.WriteLine();
    Console.WriteLine("---------------------------");
}

void LocateSubject(List<string[]>[] SubjectsByDays)
{
    Console.WriteLine("=========================");
    Console.WriteLine("==== URČENÍ PŘEDMĚTU ====");
    Console.WriteLine("Zadejte den hledaného předmětu:");
    PrintDays();
    var day = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 5);


    var hour = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 8);
    Console.WriteLine("Vyberte hodinu hledaného předmětu (1-8)");


    try
    {
        Console.WriteLine("Předmět na zadané pozici je: " + SubjectsByDays[day - 1][hour - 1][0]);
    }
    catch
    {
        Console.WriteLine("Na zadané pozici není žádný předmět.");
    }
}

static string ChangeClassName()
{
    Console.WriteLine("=========================");
    Console.WriteLine("==== ZMĚNA OZNAČENÍ TŘÍDY ====");
    Console.Write("Zadejte nové označení třídy: ");
    string newClassName = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(newClassName))
    {
        Console.Write("Označení třídy nemůže být prázdné. Zadejte prosím znovu: ");
        newClassName = Console.ReadLine();
    }
    Console.WriteLine($"Označení třídy změněno na: {newClassName}");
    return newClassName;
}

void AddOrRemoveSubject(List<string[]> subjects)
{
    Console.WriteLine("=========================");
    Console.WriteLine("==== PŘIDÁNÍ/ODEBRÁNÍ PŘEDMĚTU ====");
    Console.WriteLine("1) Přidat předmět");
    Console.WriteLine("2) Odebrat předmět");
    var choice = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 2);

    switch (choice)
    {
        case 1:
            Console.WriteLine("Jméno předmětu který chcete přidat: ");
            string newSubjectName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(newSubjectName))
            {
                Console.Write("Název předmětu nemůže být prázdný. Zadejte prosím znovu: ");
                newSubjectName = Console.ReadLine();
            }
            Console.WriteLine("Zkratka předmětu který chcete přidat (jeden znak): ");
            string newSubjectShort = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(newSubjectShort) || newSubjectShort.Length > 1)
            {
                Console.Write($"Zkratka předmětu musí být jeden znak. Zadejte prosím znovu: ");
                newSubjectShort = Console.ReadLine();
            }
            subjects.Add([newSubjectName, newSubjectShort, $"{newSubjectName} ({newSubjectShort})"]);
            Console.WriteLine($"Přidáno: {newSubjectName}");
            break;
        case 2:
            Console.WriteLine("Zadejte číslo předmětu který chcete odebrat:");
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine($"{i + 1}) {subjects[i][2]}");
            var subjectToRemove = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, subjects.Count);

            Console.WriteLine($"Odebráno: {subjects[subjectToRemove - 1][0]}");
            subjects.RemoveAt(subjectToRemove - 1);
            break;

        // DODELAT MAZANI Z ROZVRHU
        default:
            Console.WriteLine("Neplatná volba.");
            break;
    }
}

void ChangeNumberOfHours(List<string[]> subjects, List<string[]>[] SubjectsByDays)
{
    Console.WriteLine("=========================");
    Console.WriteLine("==== ZMĚNA POČTU HODIN ====");
    Console.WriteLine("Na jaký den chcete měnit počet hodin?");
    PrintDays();
    var day = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 5);

    Console.WriteLine("Kolik hodin chcete na daný den nastavit?");
    var newHourCount = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 8);

    if (SubjectsByDays[day - 1].Count > newHourCount)
    {
        SubjectsByDays[day - 1].RemoveRange(newHourCount, SubjectsByDays[day - 1].Count - newHourCount);
    }
    for (int a = 0; a < newHourCount; a++)
    {
        Console.WriteLine("Výběr pro danou hodinu:");
        for (int j = 0; j < subjects.Count; j++)
            Console.WriteLine($"{j + 1}) {subjects[j][2]}");

        var subjectChoice = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, subjects.Count);

        SubjectsByDays[day - 1].Add(subjects[subjectChoice - 1]);
    }

    
}
void SearchSubject(List<string[]>[] SubjectsByDays, List<string[]> subjects, string className)
{
    int count = 1;
    foreach (string[] subject in subjects)
    {
        Console.WriteLine($"{count}) {subject[SHORT]}"); // 0 = subj name, 1 = short name, 2 = print format
        count++;
    }

    Console.WriteLine("Zadejte číslo předmětu který chcete najít:");
    var subjectChoice = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, subjects.Count);

    string subjectToFind = subjects[subjectChoice - 1][1];

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
                if (SubjectsByDays[i][j][SHORT] == subjectToFind)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("|" + SubjectsByDays[i][j][SHORT] + "|");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("|" + SubjectsByDays[i][j][SHORT] + "|");
                }
            else
            {
                break;
            }

        }
        Console.WriteLine();
        Console.WriteLine("---------------------------");
    }
}

void SaveSchedule(List<string[]>[] SubjectsByDays, List<string[]> subjects, string className)
{
    var newSchedule = new // objekt ktery ukladane
    {
        ClassName = className,
        Subjects = subjects,
        Schedule = SubjectsByDays
    };

    object[] allSchedules; // vsechny ulozene rozvrhy

    if (File.Exists("schedule_save.json")) // vytahneme jiz ulozene rozvrhy
    {
        string existing = File.ReadAllText("schedule_save.json");


        JsonElement[] temp = JsonSerializer.Deserialize<JsonElement[]>(existing);

        allSchedules = temp.Cast<object>().ToArray();
        

    }
    else
    {
        allSchedules = Array.Empty<object>();
    }
    /// pridame novy rozvrh do vsech rozvrhu
    allSchedules = allSchedules.Append((object)newSchedule).ToArray();

    
    string json = JsonSerializer.Serialize(allSchedules, new JsonSerializerOptions
    {
        WriteIndented = true
    });

    File.WriteAllText("schedule_save.json", json);

    Console.WriteLine("Rozvrh uložen.");
}

void MainMenu(List<string[]> subjects, List<string[]>[] SubjectsByDays, string className)
{
    bool running = true;
    while (running)
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

        var choice = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 1, 12);

        switch (choice)
        {
            case 1:
                EditSchedule(subjects, SubjectsByDays);
                break;
            case 2:
                ViewSchedule(SubjectsByDays, className);
                break;
            case 3:
                ShowSubjects(subjects);
                break;
            case 4:
                ViewScheduleByDay(SubjectsByDays);
                break;
            case 5:
                LocateSubject(SubjectsByDays);
                break;
            case 6:
                System.Environment.Exit(1);
                break;
            case 7:
                AddOrRemoveSubject(subjects);
                break;
            case 8:
                className = ChangeClassName();
                break;
            case 9:
                ChangeNumberOfHours(subjects, SubjectsByDays);
                break;
            case 10:
                SearchSubject(SubjectsByDays, subjects, className);
                break;
            case 11:
                SaveSchedule(SubjectsByDays, subjects, className);
                break;
            case 12:
                running = false;
                break;
            default:
                Console.WriteLine("Neplatná volba.");
                break;
        }
    }
    

}




bool running = true;
while (running)
{
    Console.WriteLine("==== ROZVRH ====");
    Console.WriteLine("1) Vytvořit nový rozvrh - volba tvorby rozvrhu.");
    Console.WriteLine("2) Odstranit již vytvořený rozvrh - odstranění uloženého rozvrhu. (BONUSOVÁ ČÁST - NEPOVINNÉ)");
    Console.WriteLine("3) Zobrazit již vytvořený rozvrh - zobrazení uloženého rozvrhu. (BONUSOVÁ ČÁST - NEPOVINNÉ)");
    Console.WriteLine("4) Ukončit aplikaci - volba pro ukončení aplikace");

    var choice = GetIntInput("Neplatné číslo, zadejte prosím znovu:", 0, 4);

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
}







