using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Collections.Grafo
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Inserisci nodi grafo con la seguente struttura:");
            Console.WriteLine("(nodo,nodo,valoreNumericoIntero)");
            string nodi = Console.ReadLine();
            Console.WriteLine();
            bool toStop = false;

            do
            {
                Console.Write("Nodo sorgente: ");
                string n1 = Console.ReadLine();

                Console.Write("Nodo finale/obbiettivo: ");
                string nF = Console.ReadLine();

                MyLinkedList link =
                    new MyLinkedList(new MyLinkedListNode(n1, new Dictionary<string, int>()),
                    new MyLinkedListNode(nF, new Dictionary<string, int>()));

                link.AddString(nodi);

                Console.WriteLine();
                Console.WriteLine("Percorsi possibili:");
                Console.WriteLine();

                foreach (var list in link)
                {
                    int valueTot = 0;
                    string toprint = "";
                    MyLinkedListNode PrevNode = null;

                    foreach (var node in list)
                    {
                        toprint += node.ToString();

                        if (PrevNode != null)
                        {
                            valueTot += PrevNode.value[node.name];
                            PrevNode = node;
                        }
                        else
                        { PrevNode = node; }
                    }
                    
                    Console.WriteLine($"{toprint} ={valueTot}");
                }

                Console.WriteLine();
                Console.Write("Vuoi continuare, si o no?");
                if (Console.ReadLine() == "no")
                { toStop = true; }

            } while (toStop == false);
        }
    }
}
