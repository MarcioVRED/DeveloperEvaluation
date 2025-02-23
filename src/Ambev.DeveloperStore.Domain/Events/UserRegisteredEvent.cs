using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Domain.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
