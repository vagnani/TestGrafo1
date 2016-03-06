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
            Console.Write("Nodo radice: nome-");
            string n1 = Console.ReadLine();

            Console.Write("Nodo finale/obbiettivo: nome-");
            string nF = Console.ReadLine();

            MyLinkedList link =
                new MyLinkedList(new MyLinkedListNode(n1, new Dictionary<string, int>()),
                new MyLinkedListNode(nF, new Dictionary<string, int>()));

            Console.WriteLine("Inserisci nodi grafo con la seguente struttura:");
            Console.WriteLine("(nodoPadre,nodoFiglio,valoreNumericoIntero)");            
            link.AddString(Console.ReadLine());

            do
            {
                Console.WriteLine("passante per qualche nodo?se si scrivi il nome del nodo, se no scrivi 'no'");
                string str = Console.ReadLine().Trim();                

                if (str != "no")
                {
                    List<List<MyLinkedListNode>> nodeFiltered = new List<List<MyLinkedListNode>>();

                    foreach (var list in link)
                    {
                        foreach (var element in list)
                        {
                            if (element.name == str)
                            {
                                nodeFiltered.Add(list); break;
                            }
                        }
                    }

                    foreach (var list in nodeFiltered)
                    {
                        string toprint = "";
                        foreach (var node in list)
                        {
                            toprint += node.ToString();
                        }
                        Console.WriteLine(toprint);
                    }
                }

                else
                {
                    foreach (var list in link)
                    {
                        string toprint = "";
                        foreach (var node in list)
                        {
                            toprint += node.ToString();
                        }
                        Console.WriteLine(toprint);
                    }
                }

                Console.WriteLine("Vuoi continuare?");
                string toContinue = Console.ReadLine();
                if(toContinue=="no")
                {
                    break;
                }

            } while (true);

            Console.ReadKey();
        }
    }
}
