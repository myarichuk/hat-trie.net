using System;
using System.Runtime.InteropServices;

namespace hattrie.net
{
    //20 bytes size
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct ahtable_t
    {
        /* these fields are reserved for hattrie to fiddle with */
        public byte flag;
        public byte c0;
        public byte c1;

        public uint n;        // number of slots
        public uint m;        // number of key/value pairs stored
        public uint max_m;    // number of stored keys before we resize

        public uint* slot_sizes;
        public byte** slots;
    }
}
