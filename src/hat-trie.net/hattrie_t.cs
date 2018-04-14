﻿using System;
using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct hattrie_t
    {
        public node_ptr root; // root node
        public UInt32 m;      // number of stored keys
    }
}
