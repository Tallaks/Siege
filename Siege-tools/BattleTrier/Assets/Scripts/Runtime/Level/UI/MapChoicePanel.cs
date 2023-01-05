using Kulinaria.Tools.BattleTrier.Runtime.Data;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.UI
{
  public class MapChoicePanel : MonoBehaviour
  {
    [SerializeField] private MapChoiceButton _prefabChoice;
    [SerializeField] private Transform _scrollViewContainer;

    public void Initialize()
    {
      BoardData[] allMaps = Resources.LoadAll<BoardData>("Boards");
      foreach (BoardData map in allMaps)
      {
        MapChoiceButton choice = Instantiate(_prefabChoice, _scrollViewContainer);
        choice.Initialize(map);
      }
    }

    public void Show() => 
      gameObject.SetActive(true);
  }
}