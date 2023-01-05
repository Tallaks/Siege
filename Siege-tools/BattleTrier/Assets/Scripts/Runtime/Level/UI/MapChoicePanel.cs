using Kulinaria.Tools.BattleTrier.Runtime.Data;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class MapChoicePanel : MonoBehaviour
  {
    [SerializeField] private MapChoiceButton _prefabChoice;
    [SerializeField] private Transform _scrollViewContainer;

    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container) => 
      _container = container;

    public void Initialize()
    {
      BoardData[] allMaps = Resources.LoadAll<BoardData>("Boards");
      foreach (BoardData map in allMaps)
      {
        var choice = _container.InstantiatePrefab(_prefabChoice.gameObject, _scrollViewContainer).GetComponent<MapChoiceButton>();
        choice.Initialize(map);
      }
    }

    public void Show() =>
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);
  }
}