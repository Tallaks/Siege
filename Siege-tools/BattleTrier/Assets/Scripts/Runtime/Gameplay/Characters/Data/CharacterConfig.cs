using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Character")]
  public class CharacterConfig : SerializedScriptableObject
  {
    private const string ConfigsCharactersPath = "Configs/Characters/";

    [ValidateInput(nameof(CheckIds))]
    public int Id;
    [PreviewField(100, ObjectFieldAlignment.Left)]
    [HideLabel]
    public Sprite Icon;

    public string Name;
    [ValidateInput(nameof(CheckActionPoints), "ОД меньше или равно нулю")]
    public int ActionPoints;
    [ValidateInput(nameof(CheckHealthPoints), "ХП меньше или равно нулю")]
    public int HealthPoints;

    [Required] [AssetSelector]
    public Character Prefab;

    private bool CheckActionPoints() =>
      ActionPoints > 0;
    private bool CheckHealthPoints() =>
      HealthPoints > 0;

    private bool CheckIds()
    {
      CharacterConfig[] configs = Resources.LoadAll<CharacterConfig>(ConfigsCharactersPath);
      return configs.Select(k => k.Id).Distinct().Count() == configs.Select(k => k.Id).Count();
    }
  }
}