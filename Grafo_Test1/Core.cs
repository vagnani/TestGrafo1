using MyLibrary.Collections;
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

        public MyLinkedList(MyLinkedListNode first, MyLinkedListNode last)
        {
            _first = first; _goal = last;
            _allNode = new List<MyLinkedListNode>();
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
        }
        
        public bool MoveNext()
        {
            if(_index<0) //_listMax.Count<1
            {
                _listMax.Add(new List<MyLinkedListNode>() { _first });
                SetAll(_first, new List<MyLinkedListNode>() { _first}, _listMax.Count-1);
                CheckRightList();
            }
            _index++;
            
            if (_index >= _finalList.Count)
                return false;
            return true;
        }

        private void CheckRightList()
        {
            foreach(var node in _listMax)
            {
                if (node[node.Count - 1].Equals(_last))
                { _finalList.Add(node); }
            }
        }

        private void SetAll(MyLinkedListNode _first, List<MyLinkedListNode> locked, int index)
        {            
            var copyListMax = CopyFrom(_listMax[index]);            
            int copyIndex = index;

            foreach(var node in _first._next)
            {
                List<MyLinkedListNode> copyLocked = CopyFrom(locked);

                if (locked.Contains(node,node)==false)
                {
                    if(copyIndex!=index)
                    {
                        _listMax.Add(new List<MyLinkedListNode>(copyListMax));
                        copyIndex = _listMax.Count - 1;
                    }

                    _listMax[copyIndex].Add(node);
                    copyLocked.Add(node);
                    SetAll(node, copyLocked, copyIndex);
                    index++;
                }
            }
        }

        public void Reset()
        {
            _index = -1;
        }

        private T CopyFrom<T>(T list)where T:class, IEnumerable,ICollection,IList ,new ()
        {
            T result = new T();
            foreach(var n in list)
            {
                result.Add(n);
            }
            return result;
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
        internal List<MyLinkedListNode> _prev=new List<MyLinkedListNode>();        
        internal readonly string name;
        public Dictionary<string, int> value;

        public MyLinkedListNode(string name, Dictionary<string, int> value=null)
        {
            this.name = name;
            this.value = value;
        }

        public MyLinkedListNode Next(int n)
        {
            return _next[n];
        }
        public MyLinkedListNode Previous(int n)
        {
            return _prev[n];
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
        public int PreviousCount
        {
            get
            {
                return _prev.Count;
            }
        }
    }
}

