using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct hattrie_iter_t
    {
        public char* key;
        public uint keysize; // space reserved for the key
        public uint level;

        /* keep track of keys stored in trie nodes */
        public bool    has_nil_key;
        public uint nil_val;

        public hattrie_t* T;
        public bool sorted;
        ahtable_iter_t* i;
        public hattrie_node_stack_t* stack;
    }
}
