using NUnit.Framework;

namespace Tesseract.Tests
{
    [TestFixture]
    public class BaseApiTests
    {
        [Test]
        public void CanGetVersion()
        {
            var version = Interop.TessApi.BaseApiGetVersion();
            Assert.That(version, Does.StartWith("4.1.1"));
        }
    }
}