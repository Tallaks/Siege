using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class RoleChoicePanel : MonoBehaviour
  {
    [SerializeField] private Button _spectatorButton;
    [SerializeField] private Button _secondPlayerButton;
    
    private LevelMediator _levelMediator;

    [Inject]
    private void Construct(LevelMediator levelMediator) => 
      _levelMediator = levelMediator;

    public void Initialize()
    {
      _spectatorButton.onClick.AddListener(() =>
      {
      });

      _secondPlayerButton.onClick.AddListener(() =>
      {
        _levelMediator.RegisterSecondPlayer();
        _levelMediator.HideRoleChoice();
      });
    }

    public void Show() =>
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);
  }
}