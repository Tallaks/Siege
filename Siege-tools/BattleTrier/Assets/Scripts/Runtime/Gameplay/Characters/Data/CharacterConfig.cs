using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Weapons.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Character", order = 0)]
  public class CharacterConfig : SerializedScriptableObject
  {
    private const string ConfigsCharactersPath = "Configs/Characters/";

    [ValidateInput(nameof(CheckActionPoints), "ОД меньше или равно нулю")]
    public int ActionPoints;

    [ValidateInput(nameof(CheckHealthPoints), "ХП меньше или равно нулю")]
    public int HealthPoints;

    [PreviewField(100, ObjectFieldAlignment.Left)] [HideLabel]
    public Sprite Icon;

    [ValidateInput(nameof(CheckIds))]
    public int Id;

    public string Name;

    [Required] [AssetSelector]
    public Character Prefab;

    [ValidateInput(nameof(CheckWeapons))] [TableList(DrawScrollView = true)]
    public WeaponConfig[] Weapons;

    private bool CheckActionPoints() =>
      ActionPoints > 0;

    private bool CheckHealthPoints() =>
      HealthPoints > 0;

    private bool CheckIds()
    {
      CharacterConfig[] configs = Resources.LoadAll<CharacterConfig>(ConfigsCharactersPath);
      return configs.Select(k => k.Id).Distinct().Count() == configs.Select(k => k.Id).Count();
    }

    private bool CheckWeapons() =>
      Weapons is
      {
        Length: > 0
      };
  }
}