using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TestingContext.Artifact;
using TestingContext.Listener;

namespace TestingContext.Tool.IssueTrackerTools
{
    public class FlatFileIssueTracker : IssueTracker
    {
        private readonly string _path;
        private List<Issue> _issues;
        private FileSystemWatcher _watcher;

        public FlatFileIssueTracker(TestContext context, string path) : base(context)
        {
            _path = path;

            _watcher = new FileSystemWatcher
            {
                Path = ".",
                Filter = path,
                NotifyFilter = NotifyFilters.LastWrite
            };
            _watcher.Changed += new FileSystemEventHandler(OnChanged);

            _issues = File.Exists(path) ? ReadFile() : SaveFile();
        }

        public override List<Issue> GetIssues(TestContext context)
        {
            return ReadFile();
        }

        public override Issue SaveIssue(TestContext context, Issue issue)
        {
            issue.Id = Guid.NewGuid().ToString();
            _issues.Add(issue);
            SaveFile();
            return issue;
        }

        private List<Issue> ReadFile()
        {
            _watcher.EnableRaisingEvents = false;
            using (StreamReader r = new StreamReader(_path))
            {
                _issues = JsonConvert.DeserializeObject<List<Issue>>(r.ReadToEnd());
            }
            _watcher.EnableRaisingEvents = true;
            return _issues;
        }

        private List<Issue> SaveFile()
        {
            if (_issues == null)
                _issues = new List<Issue>();

            _watcher.EnableRaisingEvents = false;
            using (StreamWriter w = new StreamWriter(_path))
            {
                w.Write(JsonConvert.SerializeObject(_issues));
            }
            _watcher.EnableRaisingEvents = true;
            return _issues;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            List<Issue> before = new List<Issue>(_issues);
            ReadFile();
            List<Issue> after = _issues.Except(before).ToList();

            foreach (Issue i in after)
            {
                TestingContext.CallArtifactListeners(
                    before.Any(b => b.Id == i.Id) ? ArtifactEventType.UpdatedIssue : ArtifactEventType.NewIssue, i);
            }
        }

    }
}