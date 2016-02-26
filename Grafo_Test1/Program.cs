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
            MyLinkedList link = 
                new MyLinkedList(new MyLinkedListNode ("n1",new Dictionary<string, int>() { }),
                new MyLinkedListNode("nF",new Dictionary<string, int>() { ["n5"]=10,["n4"]=10}));

            string str = Console.ReadLine();
            link.AddString(str);
            //line to insert
            //(n1,n2,10)(n1,n3,20)(n2,n4,40)(n3,n5,50)(n4,n5,10)(n5,n4,10)(n4,nF,50)(n5,nF,10)

            //MyLinkedListNode n2 = new MyLinkedListNode("n2");
            //MyLinkedListNode n3 = new MyLinkedListNode("n3");
            //MyLinkedListNode n4 = new MyLinkedListNode("n4");
            //MyLinkedListNode n5 = new MyLinkedListNode("n5");

            //link.Add(n2);
            //link.Add(n3);
            //link.AddAfter(n2, n4);
            //link.AddAfter(n3, n5);
            //link.AddAfter(n4, n5);
            //link.AddAfter(n5, n4);
            //link.AddAfter(n4, link._goal);
            //link.AddAfter(n5, link._goal);


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
