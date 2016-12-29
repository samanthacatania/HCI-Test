using TestingContext.Runtime;

namespace TestingContext.Context
{
    internal class Runtime
    {
        private SystemUnderTest SystemUnderTest { get; set; }
        private Tester Tester { get; set; }
        private Environment Environment { get; set; }
    }
}