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


            foreach (var list in link)
            {
                string toprint = "";
                foreach(var node in list)
                {
                    toprint += node.ToString();
                }
                Console.WriteLine(toprint);
            }
            
            Console.ReadKey();
        }
    }
}
