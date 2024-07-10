using Easebnb.Shared.Entities;
using MediatR;

namespace Easebnb.Shared
{
    public class MediatRDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public MediatRDomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task DispatchAndClearEvents<TId>(IEnumerable<EntityAggregateBase<TId>> entitiesWithEvents) where TId : IEquatable<TId>
        {
            foreach (var entity in entitiesWithEvents)
            {
                var @events = entity.DomainEvents.ToArray();
                entity.ClearDomainEvents();
                foreach (var @event in @events)
                {
                    await _mediator.Publish(@event);
                }    
            }
        }
       
    }
}
