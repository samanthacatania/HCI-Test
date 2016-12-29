using System.Collections.Generic;
using TestingContext.Artifact;

namespace TestingContext.Tool
{
    public abstract class IssueTracker
    {
        public TestContext TestingContext { get; set; }

        protected IssueTracker(TestContext testingContext)
        {
            TestingContext = testingContext;
        }

        public abstract List<Issue> GetIssues(TestContext context);
        public abstract Issue SaveIssue(TestContext context, Issue issue);
    }
}