using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Cooldown
{
  public class RateLimitCooldown
  {
    private float _cooldownFinishedTime;

    public float CooldownTimeLength { get; }

    public bool CanCall => Time.unscaledTime > _cooldownFinishedTime;

    public RateLimitCooldown(float cooldownTimeLength)
    {
      CooldownTimeLength = cooldownTimeLength;
      _cooldownFinishedTime = -1f;
    }

    public void PutOnCooldown() =>
      _cooldownFinishedTime = Time.unscaledTime + CooldownTimeLength;
  }
}