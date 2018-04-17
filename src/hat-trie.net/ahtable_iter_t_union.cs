using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct ahtable_iter_t_union
    {
        [FieldOffset(0)]
        public ahtable_unsorted_iter_t* unsorted;

        [FieldOffset(0)]
        public ahtable_sorted_iter_t* sorted;
    }
}
