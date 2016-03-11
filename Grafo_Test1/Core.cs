using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace MyLibrary.Collections.Grafo
{
    public class MyLinkedList : IEnumerable<List<MyLinkedListNode>>
    {
        internal MyLinkedListNode _first;
        internal MyLinkedListNode _goal;
        internal List<MyLinkedListNode> _allNode;
        internal List<string> _allNodeString;

        public MyLinkedList(MyLinkedListNode first, MyLinkedListNode last)
        {
            _first = first; _goal = last;
            _allNode = new List<MyLinkedListNode>();
            _allNodeString = new List<string>();
            _allNode.Add(first);_allNode.Add(last);
        }
        
        public MyLinkedListNode First
        {
            get
            {
                return _first;
            }
        }
        public MyLinkedListNode Goal
        {
            get
            {
                return _goal;
            }
        }

        public IEnumerator<List<MyLinkedListNode>> GetEnumerator()
        {
            return (IEnumerator<List<MyLinkedListNode>>)(new MyEnumerator(this));
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddAfter(MyLinkedListNode node, MyLinkedListNode newNode)
        {
            var found = _allNode.FindIndex(x => x.Equals(node));
            _allNode[found]._next.Add(newNode);
            if (!_allNode.Contains(newNode, newNode))
                _allNode.Add(newNode);
        }        

        public void Add(MyLinkedListNode node)
        {
            _first._next.Add(node);
            if (!_allNode.Contains(node, node))
                _allNode.Add(node);
        }

        public void AddLast(MyLinkedListNode node)
        {
            node._next.Add(_goal);
        }

        public void AddString(string str)
        {
            str.Trim();            

            char excluded1 = '(';
            char excluded2 = ')';
            char excluded3 = ',';

            List<List<string>> all = new List<List<string>>();

            #region da string a oggetto
            for(int index=0; index<str.Count();index++)
            {        
                List<string> temp = new List<string>();

                //if (str[index] == excluded1)                
                string name = "";
                for (; index < str.Count(); index++)
                {                    
                    if (str[index] != excluded1 && str[index] != excluded2 && str[index] != excluded3) //exception
                    {
                        name += str[index];
                    }

                    if (str[index] == excluded3)
                    {
                        temp.Add(name);
                        name = "";
                    }                    

                    if (str[index] == excluded2)
                    {
                        temp.Add(name);
                        break;
                    }
                }

                all.Add(temp);
            }
            #endregion

            List<string> mustContain = new List<string>() { _first.name };
            int countDone = 0;
            List<List<string>> listDone = new List<List<string>>();
                   
            //viene fermata dalla variabile countDone (variabili fatte)
            for (int indexMust=0;countDone<all.Count; indexMust++)
            {                
                List<List<string>> rightToAdd = new List<List<string>>();                

                /*il metodo funziona per filtri, mi spiego meglio:
                prende tutti gli elementi che hanno come padre in ordine un elemento di 
                mustcontain (inizialmente nodo sorgente), aggiunge i nodi figli concatenandoli
                poi logicamente continua a filtrare la list all affinche mi dia gli elementi a sua volta
                collegati ai precedenti figli e ripete il processo fino alla fine. Insomma a ogni ripetizione
                i figli precedenti diventano come dei padri e quindi i 'nonni' sono esclusi.
                */
                foreach (var list in all)
                {                    
                    if((list[0]==mustContain[indexMust] || list[1] == mustContain[indexMust]) &&
                        listDone.Contains(list)==false)
                    {
                        rightToAdd.Add(list);                        
                    }
                }

                // una volta trovati tutti i figli di un padre si passa alla loro aggiunta definitiva
                if (rightToAdd.Count>0)
                {                                   
                    foreach (var pseudolist in rightToAdd)
                    {
                        listDone.Add(pseudolist);
                        var list = CopyFrom(pseudolist);
                        SetAllNodeString();

                        if (list[0].Equals(_goal.name) | list[1].Equals(_goal.name))
                        {
                            if (list[0].Equals(_goal.name))
                            {
                                string copy = list[0];
                                list[0] = list[1];
                                list[1] = copy;
                            }

                            if (_allNodeString.Contains(list[0]) == false)
                            {
                                _allNode.Add(new MyLinkedListNode(list[0], new Dictionary<string, int>()));
                                SetAllNodeString();
                            }
                        }
                        else if (_allNodeString.Contains(list[0])==false)
                        {
                            string copy = list[0];
                            list[0] = list[1];
                            list[1] = copy;
                        }
                        

                        //il nodo padre deve sempre esistere, per trovare il suo indice passo per una list string
                        int indexFather = _allNodeString.FindIndex(x => x == list[0]);

                        //se esiste gia il figlio passo all'aggiunta direttamente, se no lo creo
                        if (_allNodeString.Contains(list[1]))
                        {
                            //trovo indice del figlio
                            int indexSon = _allNodeString.FindIndex(x => x == list[1]);

                            //aggiunta definitiva - non simmetrica
                            AddAfter(_allNode[indexFather], _allNode[indexSon]);
                            _allNode[indexFather].value.Add(list[1], Convert.ToInt32(list[2]));

                            if (!_allNode[indexFather].Equals(_first) && !_allNode[indexSon].Equals(_goal))
                            {
                                //aggiunta definitiva - simmetrica
                                AddAfter(_allNode[indexSon], _allNode[indexFather]);
                                _allNode[indexSon].value.Add(list[0], Convert.ToInt32(list[2]));
                            }
                            
                            if (mustContain.Contains(list[1]) == false && list[1].Equals(_goal.name)==false)
                            {
                                //aggiungo il corrente figlio come futuro padre
                                mustContain.Add(list[1]);
                            }

                            //indico da escludere questo elemento
                            countDone++;
                        }
                        else
                        {
                            var node = new MyLinkedListNode(list[1], new Dictionary<string, int>());

                            //aggiunta definitiva - non simmetrica
                            AddAfter(_allNode[indexFather], node);
                            _allNode[indexFather].value.Add(list[1], Convert.ToInt32(list[2]));

                            if (!_allNode[indexFather].Equals(_first) && !node.Equals(_goal))
                            {  //aggiunta definitiva - simmetrica
                                AddAfter(node, _allNode[indexFather]);
                                node.value.Add(list[0], Convert.ToInt32(list[2]));
                            }

                            if (mustContain.Contains(list[1]) == false)
                            {
                                //aggiungo il corrente figlio come futuro padre
                                mustContain.Add(list[1]);
                            }

                            //indico da escludere questo elemento
                            countDone++;
                        }
                    }                    
                }
            }
        }
        private T CopyFrom<T>(T list) where T : class, IEnumerable, ICollection, IList, new()
        {
            T result = new T();
            foreach (var n in list)
            {
                result.Add(n);
            }
            return result;
        }
        private void SetAllNodeString()
        {
            _allNodeString = new List<string>();
            foreach(var str in _allNode)
            {
                _allNodeString.Add(str.name);
            }
        }        
    }

    public class MyEnumerator: IEnumerator<List<MyLinkedListNode>>
    {
        private MyLinkedListNode _first;
        private MyLinkedListNode _last;
        private int _index=-1;

        private List<List<MyLinkedListNode>> _listMax=new List<List<MyLinkedListNode>>();
        private List<List<MyLinkedListNode>> _finalList=new List<List<MyLinkedListNode>>();

        public List<MyLinkedListNode> Current
        {
            get
            {
                if(_index<_finalList.Count)
                    return _finalList[_index];
                throw new IndexOutOfRangeException();
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
              
        public MyEnumerator(MyLinkedList mylink)
        {
            this._first = mylink._first;
            _last = mylink._goal;

            _listMax.Add(new List<MyLinkedListNode>() { _first });
            SetAll(_first, new List<MyLinkedListNode>() { _first }, _listMax.Count - 1);
            CheckRightList();
        }
        
        public bool MoveNext()
        {
            if (_index < _finalList.Count - 1)//insicuro, fare + test
            {
                _index++;
                return true;
            }
            return false;
        }
        
        private void SetAll(MyLinkedListNode first, List<MyLinkedListNode> locked, int index)
        {            
            var copyListMax = CopyFrom(_listMax[index]);            
            int copyIndex = index;

            if (first.Equals(_last)==false)
            {
                foreach (var node in first._next)
                {                    
                    if (locked.Contains(node, node) == false)
                    {
                        List<MyLinkedListNode> copyLocked = CopyFrom(locked);
                        if (copyIndex != index)
                        {
                            _listMax.Add(new List<MyLinkedListNode>(copyListMax));
                            copyIndex = _listMax.Count - 1;
                        }

                        _listMax[copyIndex].Add(node);                        

                        if (!node.Equals(_last))
                        {
                            copyLocked.Add(node);
                            SetAll(node, copyLocked, copyIndex);
                        }

                        index=-1;
                    }
                }
            }
        }
        private T CopyFrom<T>(T list) where T : class, IEnumerable, ICollection, IList, new()
        {
            T result = new T();
            foreach (var n in list)
            {
                result.Add(n);
            }
            return result;
        }
        private void CheckRightList()
        {
            foreach (var node in _listMax)
            {
                if (node[node.Count - 1].Equals(_last))
                { _finalList.Add(node); }
            }
        }

        public void Reset()
        {
            _index = -1;
        }        

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MyEnumerator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    
    public class MyLinkedListNode:IEqualityComparer<MyLinkedListNode>
    {
        internal List<MyLinkedListNode> _next=new List<MyLinkedListNode>();
              
        internal readonly string name;
        internal Dictionary<string, int> value;

        public MyLinkedListNode(string name, Dictionary<string, int> value=null)
        {
            this.name = name;
            this.value = value;
        }

        public MyLinkedListNode Next(int n)
        {
            return _next[n];
        }        

        public override bool Equals(object obj)
        {
            MyLinkedListNode external = obj as MyLinkedListNode;
            if (this.name.Equals(external.name))
            { return true; }

            return false;
        }
        public override string ToString()
        {
            return $"({name})";
        }

        #region interface
        public bool Equals(MyLinkedListNode x, MyLinkedListNode y)
        {
            if (x.name.Equals(y.name))
            { return true; }

            return false;
        }
        public int GetHashCode(MyLinkedListNode obj)
        {
            return obj.name.GetHashCode();
        }
        #endregion

        public int NextCount
        {
            get
            {
                return _next.Count;
            }
        }        
    }
}

