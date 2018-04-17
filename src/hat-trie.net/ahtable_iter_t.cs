using System.Runtime.InteropServices;

namespace hattrie.net
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ahtable_iter_t
    {
        bool sorted;
        public ahtable_iter_t_union i;
    }
}
