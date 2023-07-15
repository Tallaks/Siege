using System.Collections.Generic;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterPlacementUi : MonoBehaviour
  {
    [SerializeField] [Required] [AssetSelector]
    private PlacementCharItem _charItemPrefab;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Transform _listContent;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private GameObject _activePlayerPanel;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private GameObject _waitingPlayerPanel;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private GameObject _spectatorPanel;

    [SerializeField] [Required] [ChildGameObjectsOnly]
    private Button _readyButton;

    public bool IsActivePanel =>
      _activePlayerPanel.activeInHierarchy;

    public bool IsWaitingPanel =>
      _waitingPlayerPanel.activeInHierarchy;

    private CharacterRegistryNetwork _characterRegistryNetwork;
    private DiContainer _container;
    private PlacementStateNetwork _placementStateNetwork;
    private RoleBase _role;
    private IStaticDataProvider _staticDataProvider;

    [Inject]
    private void Construct(
      DiContainer container,
      IStaticDataProvider staticDataProvider,
      PlacementStateNetwork placementStateNetwork,
      RoleBase role,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _container = container;
      _staticDataProvider = staticDataProvider;
      _placementStateNetwork = placementStateNetwork;
      _role = role;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public void ShowPlacementActivePlayerUi()
    {
      _activePlayerPanel.SetActive(true);
      _readyButton.gameObject.SetActive(false);
      _readyButton.onClick.AddListener(OnReadyButtonClicked);
      ShowActivePlayerUi();
    }

    public void ShowPlacementWaitingPlayerUi() =>
      _waitingPlayerPanel.SetActive(true);

    public void ShowPlacementSpectatorUi() =>
      _spectatorPanel.SetActive(true);

    public void UpdatePlacementList()
    {
      foreach (Transform child in _listContent)
        Destroy(child.gameObject);

      ShowActivePlayerUi();
    }

    public void ShowSubmitButton() =>
      _readyButton.gameObject.SetActive(true);

    public void HidePlacementActivePlayerUi()
    {
      _activePlayerPanel.SetActive(false);
      _readyButton.onClick.RemoveListener(OnReadyButtonClicked);
    }

    public void HidePlacementWaitingUi() =>
      _waitingPlayerPanel.SetActive(false);

    public void HideAll()
    {
      _activePlayerPanel.SetActive(false);
      _waitingPlayerPanel.SetActive(false);
      _spectatorPanel.SetActive(false);
    }

    private void OnReadyButtonClicked() =>
      _placementStateNetwork.ChangeActivePlayerFromServerRpc(_role.State.Value);

    private void ShowActivePlayerUi()
    {
      if (_role.State.Value == RoleState.ChosenFirst)
      {
        var notPlacedCharactersById = new Dictionary<int, int>();
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.FirstPlayerCharacters[i].TilePosition == Vector2.one * -100)
          {
            if (notPlacedCharactersById.ContainsKey(_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId))
              notPlacedCharactersById[_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId]++;
            else
              notPlacedCharactersById.Add(_characterRegistryNetwork.FirstPlayerCharacters[i].TypeId, 1);
          }

        foreach (KeyValuePair<int, int> character in notPlacedCharactersById)
        {
          var item = _container.InstantiatePrefabForComponent<PlacementCharItem>(_charItemPrefab, _listContent);
          item.Initialize(_staticDataProvider.ConfigById(character.Key), character.Value);
        }
      }
      else if (_role.State.Value == RoleState.ChosenSecond)
      {
        var notPlacedCharactersById = new Dictionary<int, int>();
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
          if (_characterRegistryNetwork.SecondPlayerCharacters[i].TilePosition == Vector2.one * -100)
          {
            if (notPlacedCharactersById.ContainsKey(_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId))
              notPlacedCharactersById[_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId]++;
            else
              notPlacedCharactersById.Add(_characterRegistryNetwork.SecondPlayerCharacters[i].TypeId, 1);
          }

        foreach (KeyValuePair<int, int> character in notPlacedCharactersById)
        {
          var item = _container.InstantiatePrefabForComponent<PlacementCharItem>(_charItemPrefab, _listContent);
          item.Initialize(_staticDataProvider.ConfigById(character.Key), character.Value);
        }
      }
    }
  }
}