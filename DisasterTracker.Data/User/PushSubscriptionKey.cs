namespace DisasterTracker.Data.User
{
    public class PushSubscriptionKey : BaseEntity
    {
        public Guid PushSubscriptionId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public UserPushSubscription UserPushSubscription { get; set; }
    }
}
