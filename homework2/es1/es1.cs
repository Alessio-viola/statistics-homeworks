using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        string filePath = "C:\\Users\\viola\\source\\repos\\ConsoleApp2_1\\ConsoleApp2_1\\ProfessionalLife.tsv";
        string[][] matrix = ParseTsvFile(filePath);

        // Leggi le righe successive (i dati)
        int numStatPoints = matrix.Length - 1;

        //Scelta delle 3 variabili
        int var1 = 8; //indice variabile qualitativa "Team leader or player?"
        int var2 = 4; // indice variabile quantitativa discreta "Hard Worker"
        int var3 = 18; //indice variabile quantitativa continua "Heigth"
               

        string[] datasetVar1 = new string[numStatPoints];
        string[] datasetVar2 = new string[numStatPoints];
        string[] datasetVar3 = new string[numStatPoints];

        Dictionary<string, int> frequencyVar1 = new Dictionary<string, int>();
        Dictionary<string, int> frequencyVar2 = new Dictionary<string, int>();
        Dictionary<int, int> frequencyVar3 = new Dictionary<int, int>();



        for (int i = 1; i < matrix.Length; i++)
        {
            string[] statPoint = matrix[i];
            datasetVar1[i-1] = statPoint[var1];
            datasetVar2[i-1] = statPoint[var2];
            datasetVar3[i-1] = statPoint[var3];
        }

        //LAVORO CON VAR1 Qualitativa (ho cercato di pulire i dati) "Team Leader or Player?"
        for (int i = 0; i < numStatPoints; i++) {
            string key = datasetVar1[i].ToLower().Trim();
            if (key.Contains("player"))
            {
                key = "player";
            }
            else if (key.Contains("leader"))
            {
                key = "leader";
            }
            else {
                continue;
            }
            if (frequencyVar1.ContainsKey(key))
            {
                frequencyVar1[key]++;
            }
            else
            {
                frequencyVar1[key] = 1;
            }
        }

        Console.WriteLine("Absolute and relative frequency: Team Leader or Player?");
        int sumVar1 = frequencyVar1.Values.Sum();
        foreach (var entry in frequencyVar1) {
            string key = entry.Key;
            int value = entry.Value;
            Console.WriteLine("Chiave: " + key + ", Absolute Frequency: " + value);
            double relativeFreq = (double) value / sumVar1;
            Console.WriteLine("Chiave: " + key + ", Relative Frequency: " + relativeFreq);
            double percentage = relativeFreq * 100;
            Console.WriteLine("Chiave: " + key + ",Percentage: " + percentage);

        }
        Console.WriteLine();

        //LAVORO CON VAR2 Quantitativa Discreta "Hard Worker (1-5)?"
        for (int i = 0; i < numStatPoints; i++)
        {
            string key = datasetVar2[i].Trim();
                    
            if (frequencyVar2.ContainsKey(key))
            {
                frequencyVar2[key]++;
            }
            else
            {
                frequencyVar2[key] = 1;
            }
        }

        Console.WriteLine("Absolute and relative frequency: Hard Worker (1-5)?");
        int sumVar2 = frequencyVar2.Values.Sum();
        foreach (var entry in frequencyVar2)
        {
            string key = entry.Key;
            int value = entry.Value;
            Console.WriteLine("Chiave: " + key + ", Absolute Frequency: " + value);
            double relativeFreq = (double)value / sumVar2;
            Console.WriteLine("Chiave: " + key + ", Relative Frequency: " + relativeFreq);
            double percentage = relativeFreq * 100;
            Console.WriteLine("Chiave: " + key + ",Percentage: " + percentage);
        }
        Console.WriteLine();

        //LAVORO CON VAR3 Quantitativa Continua "Height"

        //per determinare grandezza intervallo utilizzo max e min
        double max = double.MinValue;
        double min = double.MaxValue;
        List<double> listHeights = new List<double>();

        for (int i = 0; i < numStatPoints; i++)
        {
            string key = datasetVar3[i].Trim();
            double altezza;
            if (!double.TryParse(key, NumberStyles.Any, CultureInfo.InvariantCulture, out altezza))
            {
                continue;
            }
            else {
                if (altezza > max) max = altezza;
                if (altezza < min) min = altezza;
                listHeights.Add(altezza);  
            }
        }

        //Scelta del numero di sottointervalli
        int k = 10;
        double larghezzaIntervallo = Math.Round((max - min) / k, 2);

        // Inizializza il dizionario con chiavi per ciascun sottointervallo
        for (int i = 0; i < k+1; i++)//k+1 per gestire il caso di max
        {
            frequencyVar3[i] = 0;
        }

        foreach (double var in listHeights) {
            int indiceSottoIntervallo = (int)((var - min) / larghezzaIntervallo);
            frequencyVar3[indiceSottoIntervallo]++;
        }

                
        Console.WriteLine("Absolute and relative frequency: heigth?");
        int sumVar3 = frequencyVar3.Values.Sum();
        foreach (var entry in frequencyVar3)
        {
            int key = entry.Key;
            int value = entry.Value;
            double lowerbound = min + larghezzaIntervallo * key;
            double upperbound = min + larghezzaIntervallo * (key+1);
            Console.WriteLine("Intervallo " + key + " [" + lowerbound + "," + upperbound + ") -->" + " Absolute Frequency: " + value);
            double relativeFreq = (double)value / sumVar3;
            Console.WriteLine("Intervallo " + key + " [" + lowerbound + "," + upperbound + ") -->" + " Relative Frequency: " + relativeFreq);
            double percentage = relativeFreq * 100;
            Console.WriteLine("Intervallo " + key + " [" + lowerbound + "," + upperbound + ") -->" + " Percentage: " + percentage);
        }
        Console.WriteLine();

        //JOINT DISTRIBUTION 
        int num_variables = 2;
        string[] variables = new string[num_variables];
        variables[0] = "Team leader or Team player ?";
        variables[1] = "Hard Worker (0-5)";
        Dictionary<string, int> frequence_joint_distr = frequence_joint(matrix, num_variables, variables);
        
        int sum_stat_points = frequence_joint_distr.Values.Sum();

        foreach (var entry in frequence_joint_distr)
        {
            string key = entry.Key;
            double value = entry.Value;
            Console.WriteLine("Absolute: key: " + key + " value: " + value);
            double relative = value / sum_stat_points;
            Console.WriteLine("Relative: key: " + key + " value: " + relative);
            double percentage = relative * 100;
            Console.WriteLine("Percentage: key: " + key + " value: " + percentage);
        }
    }
    public static string[][] ParseTsvFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Il file TSV non esiste.");
            return null;
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            int numRows = lines.Length;

            if (numRows == 0)
            {
                Console.WriteLine("Il file TSV è vuoto.");
                return null;
            }

            string[][] matrix = new string[numRows][];

            for (int i = 0; i < numRows; i++)
            {
                string[] row = lines[i].Split('\t'); 
                matrix[i] = row;
            }

            return matrix;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file TSV: " + ex.Message);
            return null;
        }
    }
    public static Dictionary<string, int> frequence_joint(string[][] dataset, int numVarSelected, string[] variablesSelected)
    {
        int[] indexesVarSelected = new int[numVarSelected];
        int cols = dataset[0].Length;
        int rows = dataset.Length;
        Dictionary<string, int> output = new Dictionary<string, int>();
        int cont = 0;

        
        for (int i = 0; i < cols; i++)
        {

            if (variablesSelected.Contains(dataset[0][i]))
            {
                indexesVarSelected[cont] = i;
                cont++;
            }
        }
        for (int i = 1; i < rows; i++)
        {
            string[] elemento = new string[numVarSelected];
            for (int j = 0; j < numVarSelected; j++)
            {
                elemento[j] = dataset[i][indexesVarSelected[j]].ToLower().Trim();
            }
            string key = variablesSelected[0] + ":" + elemento[0];
            for (int j = 1; j < numVarSelected; j++)
            {
                key = key + " and " + variablesSelected[j] + ":" + elemento[j];
            }
            if (output.ContainsKey(key))
            {
                output[key]++;
            }
            else
            {
                output.Add(key, 1);
            }

        }

        return output;

    }
}
