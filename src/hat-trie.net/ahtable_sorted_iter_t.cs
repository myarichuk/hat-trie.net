using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ahtable_sorted_iter_t
    {
        public ahtable_t* table; // parent
        public byte* xs; // pointers to keys
        public uint i; // current key
    }
}
