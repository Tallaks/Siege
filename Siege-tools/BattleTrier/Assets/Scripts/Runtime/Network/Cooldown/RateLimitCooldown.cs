using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Cooldown
{
  public class RateLimitCooldown
  {
    private readonly float _cooldownTimeLength;
    private float _cooldownFinishedTime;

    public float CooldownTimeLength => _cooldownTimeLength;
    public bool CanCall => Time.unscaledTime > _cooldownFinishedTime;

    public RateLimitCooldown(float cooldownTimeLength)
    {
      _cooldownTimeLength = cooldownTimeLength;
      _cooldownFinishedTime = -1f;
    }

    public void PutOnCooldown() => 
      _cooldownFinishedTime = Time.unscaledTime + _cooldownTimeLength;
  }
}