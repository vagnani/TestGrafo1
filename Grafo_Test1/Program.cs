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
            string nodi="";
            Console.WriteLine("Problema del grafo: con nodi simmetrici");
            Console.WriteLine("Inserisci archi cosi: nodo,nodo,valore <--virgole obbligatorie e non scrivere la 'n' per i nodi");

            do
            {
                Console.Write("Inserisci arco (! =ferma aggiunta) --> ");                
                string arco = Console.ReadLine();
                if (arco[0] != '!')
                    nodi += arco.Trim()+"|";
                else
                    break;
                
            } while (true);

            do
            {
                Console.Write("Nodo sorgente: ");
                string n1 = Console.ReadLine().Trim();

                Console.Write("Nodo finale/obbiettivo: ");
                string nF = Console.ReadLine().Trim();

                MyLinkedList link =
                    new MyLinkedList(new MyLinkedListNode(n1, new Dictionary<string, int>()),
                    new MyLinkedListNode(nF, new Dictionary<string, int>()));

                link.AddString(nodi);

                Console.WriteLine();
                Console.WriteLine("Percorsi possibili:");
                Console.WriteLine();

                List<Tuple<List<string>, int>> sorted = new List<Tuple<List<string>, int>>();
                foreach (var list in link)
                {
                    int valueTot = 0;                    
                    List<string> temp = new List<string>();
                    //conservo il nodo precedente al fine di prendere il valore/distanza che collega i due 
                    MyLinkedListNode PrevNode = null;

                    foreach (var node in list)
                    {
                        temp.Add(node.ToString());

                        if (PrevNode != null)
                        {
                            valueTot += PrevNode.value[node.name];
                            PrevNode = node;
                        }
                        else
                        { PrevNode = node; }
                    }

                    sorted.Add(new Tuple<List<string>, int>(temp, valueTot));
                }

                sorted.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                foreach (var tupla in sorted)
                {
                    string toprint = "";
                    for (int i=0;i<tupla.Item1.Count;i++)
                    {
                        if (i == tupla.Item1.Count - 1)
                            toprint += "n" + tupla.Item1[i];
                        else
                            toprint += "n" + tupla.Item1[i] + ",";
                    }

                    Console.WriteLine($"{toprint} ={tupla.Item2}");
                }

                Console.WriteLine();
                Console.Write("Vuoi continuare, si o no? ");
                if (Console.ReadLine() == "no")
                    break;                

            } while (true);
        }        
    }
}
