using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct hattrie_node_stack_t
    {
        public byte   c;
        public uint level;

        public node_ptr node;
        public hattrie_node_stack_t* next;

    }
}
