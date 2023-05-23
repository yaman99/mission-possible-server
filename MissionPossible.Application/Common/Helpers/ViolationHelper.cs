using MissionPossible.Application.Common.Interfaces.Helpers;
using MissionPossible.Application.EventBus.Bus;
using MissionPossible.Domain.Constants;
using MissionPossible.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Helpers
{
    public class ViolationHelper : IViolationHelper
    {
        private readonly IDomainBus _dmoanBus;
        private readonly IDictionary<string, Func<ViolationContext , Task>> _violationHandlers;

        public ViolationHelper(IDomainBus dmoanBus)
        {
            _dmoanBus = dmoanBus;
            _violationHandlers = new Dictionary<string, Func<ViolationContext, Task>>
            {
                { PunishmentTypes.LackofWalletWarning, LackOfWalletWarning },
                { PunishmentTypes.SuspendAccountFromCampaigns, SuspendAccountFromCampaigns },
 
            };
        }

        public async Task HandleViolation(string type, int count, string email, Guid userId, Guid eventId)
        {
            // Register known input types and handlers.
            var context = new ViolationContext
            {
                Count = count,
                Email = email,
                UserId = userId,
                EventId = eventId
            };

            if (_violationHandlers.ContainsKey(type))
            {
                await _violationHandlers[type].Invoke(context);
                return;
            }

        }

        private async Task LackOfWalletWarning(ViolationContext context)
        {
            await _dmoanBus.RaiseEvent(new LackOfWalletBalanceViolationOccurred
            {
                Count = context.Count,
                Email = context.Email,
                Id = context.EventId,
            });
        } 
        private async Task SuspendAccountFromCampaigns(ViolationContext context)
        {
            await _dmoanBus.RaiseEvent(new AccountSuspendedForLackOfBalance
            {
                Count = context.Count,
                Email = context.Email,
                Id = context.EventId,
                User = context.UserId,
            });
        }

    }

    public class ViolationContext
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public int Count { get; set; }
    }
}
