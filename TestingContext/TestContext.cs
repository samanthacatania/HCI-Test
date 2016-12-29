using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TestingContext.Artifact;
using TestingContext.Context;
using TestingContext.Listener;
using TestingContext.Testware;
using TestingContext.Tool;

namespace TestingContext
{
    public class TestContext
    {
        private Context.Runtime Runtime { get; set; }
        private Context.Strategy TestStrategy { get; set; }

        public IssueTracker IssueTracker { get; set; }

        private List<IArtifactListener> artifactListeners = new List<IArtifactListener>();

        // Artifacts

        public List<Specification> GetSpecifications()
        {
            return new List<Specification>();
        }

        public Uri GetCode()
        {
            return new Uri("http://localhost");
        }

        public List<Document> GetDocumentation(string searchString)
        {
            return new List<Document>();
        }

        public bool SaveDocument(Document document)
        {
            return false;
        }

        public List<Issue> GetIssues()
        {
            return IssueTracker.GetIssues(this);
        }

        public Issue SaveIssue(Issue issue)
        {
            Issue savedIssue = IssueTracker.SaveIssue(this, issue);
            CallArtifactListeners(ArtifactEventType.NewIssue, savedIssue);
            return savedIssue;
        }

        public List<AcceptanceCriteria> GetAcceptanceCriteria()
        {
            return new List<AcceptanceCriteria>();
        }

        public Model GetModel()
        {
            return new Model();
        }

        // Testware

        public List<TestCase> GetTestPlan()
        {
            return new List<TestCase>();
        }

        public List<TestCase> GetTestSuite()
        {
            // Test Suite is an Instance of a Test Plan
            return new List<TestCase>();
        }

        public List<TestData> GetTestData()
        {
            return new List<TestData>();
        }

        public List<Uri> GetLogFiles()
        {
            return new List<Uri>();
        }

        public Report GetReport(string reportName)
        {
            return new Report();
        }

        public Script GetTestScript()
        {
            return new Script();
        }

        // Listeners

        public void AddArtifactListener(IArtifactListener listener)
        {
            artifactListeners.Add(listener);
        }

        public void CallArtifactListeners(ArtifactEventType type, IArtifact artifact)
        {
            foreach (IArtifactListener listener in artifactListeners)
            {
                listener.ArtifactEvent(type, artifact);
            }
        }
    }
}