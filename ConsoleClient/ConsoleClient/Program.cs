using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingContext;
using TestingContext.Artifact;
using TestingContext.Listener;
using TestingContext.Tool.IssueTrackerTools;

namespace ConsoleClient
{
    class Program : IArtifactListener
    {
        static void Main(string[] args)
        {
            TestContext context = new TestContext();
            FlatFileIssueTracker tracker = new FlatFileIssueTracker(context,"BUGS.json");
            context.IssueTracker = tracker;

            Issue issue = new Issue
            {
                Title = "Bug",
                Description = "It Happens",
                Status = Issue.IssueStatus.Open
            };
            context.AddArtifactListener(new Program());
            context.SaveIssue(issue);
            
        }

        public void ArtifactEvent(ArtifactEventType type, IArtifact artifact)
        {
            if(type == ArtifactEventType.NewIssue)
                Console.Out.WriteLine("NEW " + ((Issue)artifact).Id);
            else
                Console.Out.WriteLine("UPDATE " + ((Issue)artifact).Id);
            Console.In.ReadLine();
        }
    }
}
