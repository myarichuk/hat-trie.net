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

        public UInt32 n;        // number of slots
        public UInt32 m;        // number of key/value pairs stored
        public UInt32 max_m;    // number of stored keys before we resize

        public UInt32* slot_sizes;
        public byte** slots;
    }
}
