using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace hattrie.net
{
    public unsafe class HatTrie : IDictionary<string,uint>,IDisposable
    {
        private const int NODE_MAXCHAR = 0xff;
        private const int NODE_CHILDS = NODE_MAXCHAR + 1;

        private readonly hattrie_t* _root;

        public ICollection<string> Keys => throw new NotImplementedException();

        public ICollection<uint> Values => throw new NotImplementedException();

        public int Count
        {
            get
            {
                if(_root->m > int.MaxValue) //precaution
                    throw new InvalidOperationException($"Trie item count exceeds {int.MaxValue}, which is a maximum value IDictionary::Count can return");

                return (int)_root->m;
            }
        }

        public uint UnsignedCount => _root->m;

        public bool IsReadOnly => false;

        public uint this[string key]
        {
            get
            {
                if(!TryGetValue(key,out var val))
                    throw new KeyNotFoundException($"Item under key = {key} was not found");

                return val;
            }

            set => Add(key,value);
        }

        #region P/Invoke Definitions

        [DllImport("hat-trie.dll")]
        internal static extern hattrie_t* hattrie_create();

        [DllImport("hat-trie.dll")]
        internal static extern void hattrie_free(hattrie_t* root);

        [DllImport("hat-trie.dll")]
        internal static extern void hattrie_clear(hattrie_t* root);

        /** Find the given key in the trie, inserting it if it does not exist, and
         * returning a pointer to it's key.
         *
         * This pointer is not guaranteed to be valid after additional calls to
         * hattrie_get, hattrie_del, hattrie_clear, or other functions that modifies the
         * trie.
         */
        [DllImport("hat-trie.dll")]
        internal static extern uint* hattrie_get (hattrie_t* root, [MarshalAs(UnmanagedType.LPStr)] string key, uint len);

        /** Find a given key in the table, returning a NULL pointer if it does not
         * exist. */
        [DllImport("hat-trie.dll")]
        internal static extern uint* hattrie_tryget (hattrie_t* root, [MarshalAs(UnmanagedType.LPStr)] string key, uint len);

        //Delete a given key from trie. Returns 0 if successful or -1 if not found.
        [DllImport("hat-trie.dll")]
        internal static extern int hattrie_del (hattrie_t* root, [MarshalAs(UnmanagedType.LPStr)] string key, uint len);
        #endregion

        public HatTrie()
        {
            _root = hattrie_create();
            Debug.Assert(_root != null);
        }

        public void Add(string key, uint value) => 
            *hattrie_get(_root, key, (uint)key.Length) = value;

        public bool TryGetValue(string key, out uint value)
        {
            value = 0;

            var valuePtr = hattrie_tryget(_root, key, (uint)key.Length);
            if(valuePtr == null)
                return false;

            value = *valuePtr;
            return true;
        }        

        /// <summary>
        /// Remove specified key from the trie
        /// </summary>
        /// <returns>return true if key was deleted, false if it was not found</returns>
        public bool Remove(string key) =>
            hattrie_del(_root, key, (uint)key.Length) == 0;

        /// <summary>
        /// Clear trie contents
        /// </summary>
        public void Clear() => hattrie_clear(_root);
     
        public bool ContainsKey(string key) => TryGetValue(key, out var _);

        public void Add(KeyValuePair<string, uint> item) => Add(item.Key, item.Value);

        public bool Contains(KeyValuePair<string, uint> item)
        {
            if(!TryGetValue(item.Key, out var fetched))
                return false;

            if(item.Value != fetched)
                return false;

            return true;
        }

        public void CopyTo(KeyValuePair<string, uint>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, uint> item) => Remove(item.Key);

        public IEnumerator<KeyValuePair<string, uint>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                hattrie_free(_root);
                disposedValue = true;
            }
        }

        ~HatTrie()
        {
           Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
