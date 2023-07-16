using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Selection.Placement;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.PlacementState
{
  public class PlacementCharItem : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Image _icon;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _button;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private TMP_Text _count;

    private CharacterConfig _configById;

    private IPlacementSelection _placementSelection;

    [Inject]
    private void Construct(IPlacementSelection placementSelection) =>
      _placementSelection = placementSelection;

    public void Initialize(CharacterConfig configById, int characterCount)
    {
      _configById = configById;
      _icon.sprite = configById.Icon;
      _count.text = characterCount.ToString();
      _button.onClick.AddListener(OnCharacterSelected);
    }

    private void OnCharacterSelected() =>
      _placementSelection.SelectConfig(_configById);
  }
}