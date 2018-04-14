using hattrie.net;
using Xunit;

namespace hat_trie.net.tests
{
    public class Basics
    {
        [Fact]
        public void Can_initialize() //sanity check to make sure unmanaged DLL is ok
        {
            using (var trie = new HatTrie())
            {
            }
        }

        [Fact]
        public void Can_add_and_fetch_data()
        {
            using (var trie = new HatTrie())
            {
                trie.Add("Foo",123);
                trie.Add("Bar",345);
                trie.Add("FooBar",567);

                Assert.False(trie.TryGet("ABC", out _));
                Assert.False(trie.TryGet("FooBar1", out _));

                Assert.True(trie.TryGet("FooBar",out var fooBarValue));
                Assert.Equal((uint)567,fooBarValue);

                Assert.True(trie.TryGet("Bar",out var barValue));
                Assert.Equal((uint)345,barValue);

                Assert.True(trie.TryGet("Foo",out var fooValue));
                Assert.Equal((uint)123,fooValue);
            }
        }
    }
}
