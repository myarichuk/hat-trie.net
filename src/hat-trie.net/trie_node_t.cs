using System;
using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct trie_node_t
    {
        public byte flag;

        /* the value for the key that is consumed on a trie node */
        public UInt32 val;

        /* Map a character to either a trie_node_t or a ahtable_t. The first byte
         * must be examined to determine which. */
        public node_ptr* xs; //this is an array with constant size of 256 (0..255)
    }
}
