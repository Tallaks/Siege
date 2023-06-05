using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterPlacementUi : MonoBehaviour
  {
    [SerializeField, Required, AssetSelector] private PlacementCharItem _charItemPrefab;
    [SerializeField, Required, ChildGameObjectsOnly] private Transform _listContent;
    [SerializeField, Required, ChildGameObjectsOnly] private GameObject _activePlayerPanel;
    [SerializeField, Required, ChildGameObjectsOnly] private GameObject _waitingPlayerPanel;
    [SerializeField, Required, ChildGameObjectsOnly] private GameObject _spectatorPanel;

    public bool IsActivePanel =>
      _activePlayerPanel.activeInHierarchy;

    private ICharacterRegistry _characterRegistryLocal;

    private DiContainer _container;
    private IStaticDataProvider _staticDataProvider;

    [Inject]
    private void Construct(
      DiContainer container,
      IStaticDataProvider staticDataProvider,
      ICharacterRegistry characterRegistryLocal)
    {
      _container = container;
      _staticDataProvider = staticDataProvider;
      _characterRegistryLocal = characterRegistryLocal;
    }

    public void ShowPlacementActivePlayerUi()
    {
      _activePlayerPanel.SetActive(true);
      ShowActivePlayerUi();
    }

    public void ShowPlacementWaitingPlayerUi() =>
      _waitingPlayerPanel.SetActive(true);

    public void ShowPlacementSpectatorUi() =>
      _spectatorPanel.SetActive(true);

    private void ShowActivePlayerUi()
    {
      foreach (int characterConfigsId in _characterRegistryLocal.CharacterConfigsIds)
      {
        var listItem = _container.InstantiatePrefabForComponent<PlacementCharItem>(_charItemPrefab, _listContent);
        listItem.Initialize(_staticDataProvider.ConfigById(characterConfigsId),
          _characterRegistryLocal.Characters[characterConfigsId]);
      }
    }
  }
}