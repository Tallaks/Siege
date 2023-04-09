using Sirenix.OdinInspector;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data
{
  [CreateAssetMenu(menuName = "Kulinaria/Character")]
  public class CharacterConfig : SerializedScriptableObject
  {
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
  }
}