using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Weapons.Data
{
  [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Kulinaria/Weapon", order = 1)]
  public class WeaponConfig : SerializedScriptableObject
  {
    private const string ConfigsWeaponPath = "Configs/Weapons/";

    [ValidateInput(nameof(CheckIds))]
    public int Id;

    public string Name;

    [PreviewField(100, ObjectFieldAlignment.Left)] [HideLabel]
    public Sprite Icon;

    [Range(1, 50)] public int Range;

    [MinMaxSlider(nameof(DamageRange), true)]
    public Vector2Int MinMaxDamage = new(25, 50);

    [Range(0, 1)] public float Accuracy;

    [BoxGroup("Attack Property", ShowLabel = false)] [Range(1, 45)]
    public int AmmoCapacity;

    [BoxGroup("Attack Property", ShowLabel = false)] [Range(1, 10)]
    public int AttackActionPoints;

    [BoxGroup("Attack Property", ShowLabel = false)] [Range(1, 10)]
    public int AttackSpeedTicks;

    [BoxGroup("Reload Property", ShowLabel = false)] [Range(1, 10)]
    public int ReloadActionPoints;

    [BoxGroup("Reload Property", ShowLabel = false)] [Range(1, 10)]
    public int ReloadSpeedTicks;

    [HideInInspector] public Vector2Int DamageRange = new(1, 30);

    public int MinDamage => MinMaxDamage.x;
    public int MaxDamage => MinMaxDamage.y;

    private bool CheckIds()
    {
      WeaponConfig[] configs = Resources.LoadAll<WeaponConfig>(ConfigsWeaponPath);
      return configs.Select(k => k.Id).Distinct().Count() == configs.Select(k => k.Id).Count();
    }
  }
}