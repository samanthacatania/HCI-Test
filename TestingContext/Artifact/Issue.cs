using System;
using System.Collections.Generic;

namespace TestingContext.Artifact
{
    public class Issue : IArtifact, IEquatable<Issue>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }

        public enum IssueStatus
        {
            Open,
            InProgress,
            Resolved,
            Reopened,
            Closed
        }

        public bool Equals(Issue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && string.Equals(Title, other.Title) && string.Equals(Description, other.Description) && Status == other.Status;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Issue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (int) Status;
                return hashCode;
            }
        }

        private sealed class IssueEqualityComparer : IEqualityComparer<Issue>
        {
            public bool Equals(Issue x, Issue y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Id, y.Id) && string.Equals(x.Title, y.Title) && string.Equals(x.Description, y.Description) && x.Status == y.Status;
            }

            public int GetHashCode(Issue obj)
            {
                unchecked
                {
                    var hashCode = obj.Id?.GetHashCode() ?? 0;
                    hashCode = (hashCode * 397) ^ (obj.Title?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (obj.Description?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (int) obj.Status;
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<Issue> IssueComparer { get; } = new IssueEqualityComparer();
    }
}