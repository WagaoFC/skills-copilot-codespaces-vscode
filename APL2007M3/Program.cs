namespace ReportGenerator
{
    class QuarterlyIncomeReport
    {
        static void Main(string[] args)
        {
            // create a new instance of the class
            QuarterlyIncomeReport report = new QuarterlyIncomeReport();

            // call the GenerateSalesData method
            SalesData[] salesData = report.GenerateSalesData();

            // call the DisplayReport method
            report.QuarterlySalesReport(salesData);
        }

        /* public struct SalesData includes the following fields: date sold, department name, product ID, quantity sold, unit price */
        public struct SalesData
        {
            public DateOnly dateSold;
            public string departmentName;
            public int productID;
            public int quantitySold;
            public double unitPrice;
        }

        /* the GenerateSalesData method returns 1000 SalesData records. It assigns random values to each field of the data structure */
        public SalesData[] GenerateSalesData()
        {
            SalesData[] salesData = new SalesData[1000];
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                salesData[i].dateSold = new DateOnly(2023, random.Next(1, 13), random.Next(1, 29));
                salesData[i].departmentName = "Department " + random.Next(1, 11);
                salesData[i].productID = random.Next(1, 101);
                salesData[i].quantitySold = random.Next(1, 101);
                salesData[i].unitPrice = random.Next(1, 101) + random.NextDouble();
            }

            return salesData;
        }

        public void QuarterlySalesReport(SalesData[] salesData)
        {
            // create a dictionary to store the quarterly sales data
            Dictionary<string, double> quarterlySales = new Dictionary<string, double>();

            // iterate through the sales data
            foreach (SalesData data in salesData)
            {
                // calculate the total sales for each quarter
                string quarter = GetQuarter(data.dateSold.Month);
                double totalSales = data.quantitySold * data.unitPrice;

                if (quarterlySales.ContainsKey(quarter))
                {
                    quarterlySales[quarter] += totalSales;
                }
                else
                {
                    quarterlySales.Add(quarter, totalSales);
                }
            }

            // display the quarterly sales report
            Console.WriteLine("Quarterly Sales Report");
            Console.WriteLine("----------------------");
            foreach (KeyValuePair<string, double> quarter in quarterlySales)
            {
                Console.WriteLine("{0}: ${1}", quarter.Key, quarter.Value);
            }
        }

        public string GetQuarter(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return "Q1";
            }
            else if (month >= 4 && month <= 6)
            {
                return "Q2";
            }
            else if (month >= 7 && month <= 9)
            {
                return "Q3";
            }
            else
            {
                return "Q4";
            }
        }
    }

    private static readonly Random random = new Random();
    private const int MaxYearsBack = 10;

    static double GenerateRandomBalance(double min, double max)
    {
        double balance = random.NextDouble() * (max - min) + min;
        return Math.Round(balance, 2);
    }

    static string GenerateRandomAccountHolder()
    {
        string[] accountHolderNames = {  /* names here */  };
        var accountHolderName = accountHolderNames[random.Next(0, accountHolderNames.Length)];
        return accountHolderName;
    }

    static string GenerateRandomAccountType()
    {
        string[] accountTypes = {  /* types here */  };
        return accountTypes[random.Next(0, accountTypes.Length)];
    }

    static DateTime GenerateRandomDateOpened()
    {
        DateTime startDate = new DateTime(DateTime.Today.Year - MaxYearsBack, 1, 1);
        int daysRange = (DateTime.Today - startDate).Days;
        DateTime randomDate = startDate.AddDays(random.Next(daysRange));

        if (randomDate.Year == DateTime.Today.Year && randomDate >= DateTime.Today)
        {
            randomDate = randomDate.AddDays(-1);
        }

        return randomDate;
    }

    string[] words = {
        "as", "astronaut", "asteroid", "are", "around",
        "cat", "cars", "cares", "careful", "carefully",
        "for", "follows", "forgot", "from", "front",
        "mellow", "mean", "money", "monday", "monster",
        "place", "plan", "planet", "planets", "plans",
        "the", "their", "they", "there", "towards"
    };
    
    Trie dictionary = InitializeTrie(words);
    // SearchWord();
    // PrefixAutocomplete();
    // DeleteWord();
    // GetSpellingSuggestions();

    // Inicializa a Trie com uma lista de palavras
    Trie InitializeTrie(string[] words)
    {
        Trie trie = new Trie();

        foreach (string word in words)
        {
            trie.Insert(word);
        }

        return trie;
    }
    
    // Permite ao usuário buscar uma palavra na Trie
    void SearchWord()
    {
        while (true)
        {
            Console.WriteLine("Enter a word to search for, or press Enter to exit.");
            string? input = Console.ReadLine();
            if (input == "")
            {
                break;
            }
            /*
            if (input != null && dictionary.Search(input))
            {
                Console.WriteLine($"Found \"{input}\" in dictionary");
            }
            */
            else
            {
                Console.WriteLine($"Did not find \"{input}\" in dictionary");
            }
        }
    }
    
    // Permite ao usuário buscar palavras por prefixo na Trie
    void PrefixAutocomplete()
    {
        PrintTrie(dictionary);
        GetPrefixInput();
    }

    // Permite ao usuário deletar uma palavra da Trie
    void DeleteWord() 
    {
        PrintTrie(dictionary);
        while(true)
        {
            Console.WriteLine("\nEnter a word to delete, or press Enter to exit.");
            string? input = Console.ReadLine();
            if (input == "")
            {
                break;
            }
            /*
            if (input != null && dictionary.Search(input))
            {
                dictionary.Delete(input);
                Console.WriteLine($"Deleted \"{input}\" from dictionary\n");
                PrintTrie(dictionary);
            }
            */
            else
            {
                Console.WriteLine($"Did not find \"{input}\" in dictionary");
            }
        }
    }
    
    // Permite ao usuário obter sugestões de ortografia para uma palavra
    void GetSpellingSuggestions() 
    {
        PrintTrie(dictionary);
        Console.WriteLine("\nEnter a word to get spelling suggestions for, or press Enter to exit.");
        string? input = Console.ReadLine();
        if (input != null)
        {
            var similarWords = dictionary.GetSpellingSuggestions(input);
            Console.WriteLine($"Spelling suggestions for \"{input}\":");
            if (similarWords.Count == 0)
            {
                Console.WriteLine("No suggestions found.");
            }
            else 
            {
                foreach (var word in similarWords)
                {
                    Console.WriteLine(word);
                }
            }
        }
    }
    
    // Executa todos os métodos de exemplo
    void RunAllExercises()
    {
        SearchWord();
        PrefixAutocomplete();
        DeleteWord();
        GetSpellingSuggestions();
    }

    // Obtém a entrada do usuário para buscar palavras por prefixo
    void GetPrefixInput()
    {
        Console.WriteLine("\nEnter a prefix to search for, then press Tab to " + "cycle through search results. Press Enter to exit.");

        bool running = true;
        string prefix = "";
        StringBuilder sb = new StringBuilder();
        List<string>? words = null;
        int wordsIndex = 0;

        while(running)
        {
            var input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.Spacebar)
            {
                Console.Write(' ');
                prefix = "";
                sb.Append(' ');
                continue;
            } 
            else if (input.Key == ConsoleKey.Backspace && Console.CursorLeft > 0)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                sb.Remove(sb.Length - 1, 1);
                prefix = sb.ToString().Split(' ').Last();
            }
            else if (input.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                running = false;
                continue;
            }
            else if (input.Key == ConsoleKey.Tab && prefix.Length > 1)
            {
                string previousWord = sb.ToString().Split(' ').Last();

                if (words != null) {
                    if (!previousWord.Equals(words[wordsIndex - 1]))
                    {
                        words = dictionary.AutoSuggest(prefix);
                        wordsIndex = 0;
                    }
                } 
                else {
                    words = dictionary.AutoSuggest(prefix);
                    wordsIndex = 0;
                }

                for (int i = prefix.Length; i < previousWord.Length; i++)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    sb.Remove(sb.Length - 1, 1);
                }
            
                
                if (words.Count > 0 && wordsIndex < words.Count)
                {
                    string output = words[wordsIndex++];
                    Console.Write(output.Substring(prefix.Length));
                    sb.Append(output.Substring(prefix.Length));
                }
                continue;
            }
            else if (input.Key != ConsoleKey.Tab)
            {
                Console.Write(input.KeyChar);
                prefix += input.KeyChar;
                sb.Append(input.KeyChar);
                words = null;
                wordsIndex = 0;
            }
        }
    }

    // Imprime todas as palavras contidas na Trie
    void PrintTrie(Trie trie)
    {
        Console.WriteLine("The dictionary contains the following words:");
        List<string> words = trie.GetAllWords();
        foreach (string word in words)
        {
            Console.Write($"{word}, ");
        }
        Console.WriteLine();
    }
}