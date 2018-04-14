using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace hattrie.net
{
    public unsafe class HatTrie : IDisposable
    {
        private const int NODE_MAXCHAR = 0xff;
        private const int NODE_CHILDS = NODE_MAXCHAR + 1;

        private hattrie_t* _root;

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
        internal static extern UInt32* hattrie_get (hattrie_t* root, [MarshalAs(UnmanagedType.LPStr)] string key, UInt32 len);

        /** Find a given key in the table, returning a NULL pointer if it does not
         * exist. */
        [DllImport("hat-trie.dll")]
        internal static extern UInt32* hattrie_tryget (hattrie_t* root, [MarshalAs(UnmanagedType.LPStr)] string key, UInt32 len);

        //Delete a given key from trie. Returns 0 if successful or -1 if not found.
        [DllImport("hat-trie.dll")]
        internal static extern int hattrie_del (hattrie_t* root, [MarshalAs(UnmanagedType.LPStr)] string key, UInt32 len);
        #endregion

        public HatTrie()
        {
            _root = hattrie_create();
            Debug.Assert(_root != null);
        }

        public void Add(string key, uint value) => 
            *hattrie_get(_root, key, (uint)key.Length) = value;

        public bool TryGet(string key, out uint value)
        {
            value = 0;

            var valuePtr = hattrie_tryget(_root, key, (uint)key.Length);
            if(valuePtr == null)
                return false;

            value = *valuePtr;
            return true;
        }        

        /// <summary>
        /// Delete specified key from the trie
        /// </summary>
        /// <returns>return true if key was deleted, false if it was not found</returns>
        public bool Delete(string key) =>
            hattrie_del(_root, key, (uint)key.Length) == 0;

        /// <summary>
        /// Clear trie contents
        /// </summary>
        public void Clear() => hattrie_clear(_root);

        //TODO: make sure to implement proper Finalizer pattern
        public void Dispose()
        {
            hattrie_free(_root);
        }
    }
}
