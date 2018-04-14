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

                Assert.Equal(3,trie.Count);

                Assert.False(trie.TryGetValue("ABC", out _));
                Assert.False(trie.TryGetValue("FooBar1", out _));

                Assert.True(trie.TryGetValue("FooBar",out var fooBarValue));
                Assert.Equal((uint)567,fooBarValue);

                Assert.True(trie.TryGetValue("Bar",out var barValue));
                Assert.Equal((uint)345,barValue);

                Assert.True(trie.TryGetValue("Foo",out var fooValue));
                Assert.Equal((uint)123,fooValue);
            }
        }
    }
}
