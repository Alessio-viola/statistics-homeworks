using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Array
        Console.WriteLine("Array");
        int[] myArray = { 1, 2, 3, 4, 5 };
        // Loop
        foreach (var item in myArray)
        {
            Console.WriteLine(item);
        }
        // Add/Remove (Non è possibile per gli array)
        // Get/Set
        int valueAtIndex = myArray[2];
        Console.WriteLine("Valore nell'Array all'indice 2: " + valueAtIndex);
        // Check Existence
        bool contieneValore = myArray.Contains(3);
        Console.WriteLine("L'Array contiene 3: " + contieneValore);

        // List
        Console.WriteLine("List");
        List<int> myList = new List<int> { 1, 2, 3, 4, 5 };
        // Loop
        foreach (var item in myList)
        {
            Console.WriteLine(item);
        }
        // Add/Remove
        myList.Add(6);
        myList.Remove(2);
        // Get/Set
        int valueAtIndexList = myList[2];
        Console.WriteLine("Valore nella lista all'indice 2: " + valueAtIndexList);
        // Check Existence
        bool contieneValoreLista = myList.Contains(3);
        Console.WriteLine("La lista contiene 3: " + contieneValoreLista);

        // Dictionary
        Console.WriteLine("Dictionary");
        Dictionary<string, int> myDict = new Dictionary<string, int>
        {
            { "Uno", 1 },
            { "Due", 2 },
            { "Tre", 3 }
        };
        // Loop
        foreach (var kvp in myDict)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        // Add/Remove
        myDict["Quattro"] = 4;
        myDict.Remove("Due");
        // Get/Set
        int valorePerChiave = myDict["Tre"];
        Console.WriteLine("Valore per 'Tre': " + valorePerChiave);
        // Check Existence
        bool contieneChiave = myDict.ContainsKey("Uno");
        Console.WriteLine("Il dizionario contiene 'Uno': " + contieneChiave);

        // SortedList
        Console.WriteLine("SortedList");
        SortedList<int, string> sortedList = new SortedList<int, string>();
        sortedList.Add(3, "Tre");
        sortedList.Add(1, "Uno");
        sortedList.Add(2, "Due");
        // Loop
        foreach (var kvp in sortedList)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        // Add/Remove
        sortedList.Add(4, "Quattro");
        sortedList.Remove(2);
        // Get
        string valueForKey = sortedList[3];
        Console.WriteLine("Value for Key 3: " + valueForKey);
        // Check Existence
        bool containsKey = sortedList.ContainsKey(1);
        Console.WriteLine("SortedList Contains Key 1: " + containsKey);

        // HashSet
        Console.WriteLine("HashSet");
        HashSet<string> hashSet = new HashSet<string>();
        hashSet.Add("Apple");
        hashSet.Add("Banana");
        hashSet.Add("Orange");
        // Loop
        foreach (var item in hashSet)
        {
            Console.WriteLine(item);
        }
        // Add/Remove (No indexing)
        hashSet.Add("Grapes");
        hashSet.Remove("Banana");
        // Check Existence
        bool containsItem = hashSet.Contains("Orange");
        Console.WriteLine("HashSet Contains 'Orange': " + containsItem);

        // SortedSet
        Console.WriteLine("SortedSet");
        SortedSet<int> sortedSet = new SortedSet<int> { 3, 1, 2, 4 };
        // Loop
        foreach (var item in sortedSet)
        {
            Console.WriteLine(item);
        }
        sortedSet.Add(5);
        sortedSet.Remove(1);
        // Check Existence
        bool containsItem = sortedSet.Contains(1);
        Console.WriteLine("sortedSet Contains 1: " + containsItem);
        
        // Queue
        Console.WriteLine("Queue");
        Queue<string> queue = new Queue<string>();
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        
        while (queue.Count > 0)
        {
            string item = queue.Dequeue();
            Console.WriteLine(item);
        }

        // Stack
        Console.WriteLine("Stack");
        Stack<string> stack = new Stack<string>();
        stack.Push("A");
        stack.Push("B");
        stack.Push("C");
        
        while (stack.Count > 0)
        {
            string item = stack.Pop();
            Console.WriteLine(item);
        }

        // LinkedList
        Console.WriteLine("LinkedList");
        LinkedList<string> linkedList = new LinkedList<string>();
        var node1 = linkedList.AddLast("First");
        var node2 = linkedList.AddAfter(node1, "Second");
        var node3 = linkedList.AddAfter(node2, "Third");
        // Loop
        var currentNode = linkedList.First;
        while (currentNode != null)
        {
            Console.WriteLine(currentNode.Value);
            currentNode = currentNode.Next;
        }
        // Add/Remove (No indexing)
        linkedList.AddLast("Fourth");
        linkedList.Remove("Second");
    }
}
