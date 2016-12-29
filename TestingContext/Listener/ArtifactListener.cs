using TestingContext.Artifact;

namespace TestingContext.Listener
{
    public interface IArtifactListener
    {
        void ArtifactEvent(ArtifactEventType type, IArtifact artifact);
    }
}