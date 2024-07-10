using Easebnb.Domain.Homestay.Events;
using MediatR;

namespace Easebnb.Domain.Homestay.Handlers
{
    internal class CreateHomestayEventHandler : INotificationHandler<CreateHomestayEvent>
    {
        public Task Handle(CreateHomestayEvent notification, CancellationToken cancellationToken)
        {
            var debug = notification.HomestayId;
            return Task.CompletedTask;
        }
    }
}
