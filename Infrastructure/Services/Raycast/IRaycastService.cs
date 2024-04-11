using Codebase.Logic;
using System.Collections.Generic;

namespace Codebase.Infrastructure.Services
{
    public interface IRaycastService
    {
        bool IsTargetHit(Actor initiator, float radius, out List<ITarget> targets);
    }
}