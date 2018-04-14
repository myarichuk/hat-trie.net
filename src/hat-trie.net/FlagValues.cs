namespace hattrie.net
{
    //for tests like *node_ptr::flag & NODE_TYPE_TRIE == 0
    internal class FlagValues
    {
        public const byte NODE_TYPE_TRIE          = 0x1;
        public const byte NODE_TYPE_PURE_BUCKET   = 0x2;
        public const byte NODE_TYPE_HYBRID_BUCKET = 0x4;
        public const byte NODE_HAS_VAL            = 0x8;
    }
}
