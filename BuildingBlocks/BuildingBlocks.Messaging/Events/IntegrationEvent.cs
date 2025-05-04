namespace BuildingBlocks.Messaging.Events
{
    public record IntegrationEvent
    {
        public Guid Id => Guid.NewGuid();

        public DateTime OccurOn  => DateTime.UtcNow;

        public string EventType => GetType().AssemblyQualifiedName;
    }
}
