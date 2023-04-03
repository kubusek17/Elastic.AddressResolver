using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elastic.AddressResolver.Tests
{
    [TestClass]
    public class ResolverTests
    {
        [TestMethod]
        public void Resolve_without_Error()
        {
            Resolver resolver = new Resolver();
            var hosts = new List<string>() { "onet.pl", "o2.pl", "gmail.com" };
            resolver.Resolve(hosts);
            Assert.IsTrue(resolver.Results.Count() >= hosts.Count());

        }
        [TestMethod]
        public void Resolve_Text()
        {
            Resolver resolver = new Resolver();
            resolver.Resolve(new List<string>() { "test", "anothertest" });
            Assert.IsTrue(resolver.Results.Count() == 0);
        }
    }
}