using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines
{
  public class AggregateRunner : MonoBehaviour, ICoroutineRunner, IUpdateRunner
  {
    private readonly Queue<Action> _pendingHandlers = new();
    private readonly Dictionary<Action, SubscriberData> _subscriberData = new();
    private readonly HashSet<Action> _subscribers = new();


    private void Update()
    {
      while (_pendingHandlers.Count > 0)
        _pendingHandlers.Dequeue()?.Invoke();

      foreach (Action subscriber in _subscribers)
      {
        SubscriberData data = _subscriberData[subscriber];
        if (Time.time >= data.NextCallTime)
        {
          subscriber.Invoke();
          data.NextCallTime = Time.time + data.Period;
        }
      }
    }

    public void Subscribe(Action updateAction, float updatePeriod)
    {
      if (!_subscribers.Contains(updateAction))
        _pendingHandlers.Enqueue(() =>
        {
          if (_subscribers.Add(updateAction))
            _subscriberData.Add(updateAction, new SubscriberData
            {
              Period = updatePeriod,
              NextCallTime = 0
            });
        });
    }

    public void Unsubscribe(Action updateAction)
    {
      _pendingHandlers.Enqueue(() =>
      {
        _subscribers.Remove(updateAction);
        _subscriberData.Remove(updateAction);
      });
    }

    private class SubscriberData
    {
      public float NextCallTime;
      public float Period;
    }
  }
}