using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct node_ptr
    {
        [FieldOffset(0)]
        public ahtable_t* b;

        [FieldOffset(0)]
        public trie_node_t* t;    
        
        [FieldOffset(0)]
        public byte* flag;
    }
}
