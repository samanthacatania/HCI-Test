namespace TestingContext.Testware
{
    public class TestCase
    {
        private string Scenario { get; set; }
        private string ExcpectedOutcome { get; set; }
        private string ActualOutcome { get; set; }
        private bool TestPassed { get; set; }
    }
}