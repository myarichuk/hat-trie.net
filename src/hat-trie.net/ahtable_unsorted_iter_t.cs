using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct  ahtable_unsorted_iter_t
    {
        public ahtable_t* table; // parent
        public uint i;           // slot index
        public byte* s;           // slot position
    }
}
