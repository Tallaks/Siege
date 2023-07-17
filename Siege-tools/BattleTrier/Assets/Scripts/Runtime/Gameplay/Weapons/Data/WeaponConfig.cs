using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Weapons.Data
{
  [Serializable]
  [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Kulinaria/Weapon", order = 1)]
  public class WeaponConfig : SerializedScriptableObject
  {
    private const string ConfigsWeaponPath = "Configs/Weapons/";

    [ValidateInput(nameof(CheckIds))] [TableColumnWidth(20, false)]
    public int Id;

    [Multiline] [BoxGroup("Name", ShowLabel = false)] [HideLabel] [TableColumnWidth(200, false)]
    public string Name;

    [PreviewField(100, ObjectFieldAlignment.Left)] [HideLabel] [TableColumnWidth(100, false)]
    public Sprite Icon;

    [BoxGroup("Properties", ShowLabel = false)] [Range(1, 50)] 
    public int Range;

    [FormerlySerializedAs("MinMaxDamage")] [BoxGroup("Properties", ShowLabel = false)] [MinMaxSlider(nameof(DamageRange), true)]
    public Vector2Int Damage = new(25, 50);

    [BoxGroup("Properties", ShowLabel = false)] [Range(0, 1)]
    public float Accuracy;

    [FormerlySerializedAs("AmmoCapacity")] [BoxGroup("Properties", ShowLabel = false)] [Range(1, 45)]
    public int Capacity;

    [FormerlySerializedAs("AttackActionPoints")] [BoxGroup("Properties", ShowLabel = false)] [Range(1, 10)]
    public int AttackAP;

    [FormerlySerializedAs("AttackSpeedTicks")] [BoxGroup("Properties", ShowLabel = false)] [Range(1, 10)]
    public int AttackSpeed;

    [FormerlySerializedAs("ReloadActionPoints")] [BoxGroup("Properties", ShowLabel = false)] [Range(1, 10)]
    public int ReloadAP;

    [FormerlySerializedAs("ReloadSpeedTicks")] [BoxGroup("Properties", ShowLabel = false)] [Range(1, 10)]
    public int ReloadSpeed;

    [HideInInspector] public Vector2Int DamageRange = new(1, 30);

    public int MinDamage => Damage.x;
    public int MaxDamage => Damage.y;

    private bool CheckIds()
    {
      WeaponConfig[] configs = Resources.LoadAll<WeaponConfig>(ConfigsWeaponPath);
      return configs.Select(k => k.Id).Distinct().Count() == configs.Select(k => k.Id).Count();
    }
  }
}