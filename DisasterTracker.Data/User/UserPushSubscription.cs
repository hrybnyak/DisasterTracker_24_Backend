namespace DisasterTracker.Data.User
{
    public class UserPushSubscription : BaseEntity
    {
        public string Endpoint { get; set; }
        public Guid UserId { get; set; }

        public List<PushSubscriptionKey> PushSubscriptionKeys = new List<PushSubscriptionKey>();
        public User User { get; set; }
    }
}
