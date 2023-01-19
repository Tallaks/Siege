using System;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines
{
  public interface IUpdateRunner
  {
    void Subscribe(Action updateAction, float updatePeriod);
    void Unsubscribe(Action updateAction);
  }
}