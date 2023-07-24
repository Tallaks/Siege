using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle
{
  public class BattleUi : MonoBehaviour
  {
    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private BattleLog _battleLog;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private GameObject _activePlayerBattleUi;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private GameObject _spectatorPlayerBattleUi;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private ScrollRect _playerListScrollRect;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private ScrollRect _enemyListScrollRect;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private ScrollRect _spectatorFirstPlayerListScrollRect;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private ScrollRect _spectatorRectEnemyListScrollRect;

    [SerializeField] [Required] [AssetSelector(Paths = "Assets/Prefabs/UI")]
    private BattlePlayerListElement _playerListElementPrefab;

    [SerializeField] [Required] [AssetSelector(Paths = "Assets/Prefabs/UI")]
    private BattleEnemyListElement _enemyListElementPrefab;

    [SerializeField] [Required] [ChildGameObjectsOnly(IncludeInactive = true)]
    private TMP_Text _currentStatusText;

    private ICharacterRegistry _characterRegistry;
    private CharacterRegistryNetwork _characterRegistryNetwork;

    private IInstantiator _instantiator;
    private RoleState _role;

    [Inject]
    private void Construct(
      IInstantiator instantiator,
      ICharacterRegistry characterRegistry,
      CharacterRegistryNetwork characterRegistryNetwork)
    {
      _instantiator = instantiator;
      _characterRegistry = characterRegistry;
      _characterRegistryNetwork = characterRegistryNetwork;
    }

    public void ShowActivePlayerBattleUi()
    {
      _activePlayerBattleUi.SetActive(true);
      InitializePlayerList();
      InitializeEnemyList();
      _currentStatusText.text = "Initialization";
    }

    public void ShowSpectatorBattleUi() =>
      _spectatorPlayerBattleUi.SetActive(true);

    public void HideActivePlayerBattleUi() =>
      _activePlayerBattleUi.SetActive(false);

    public void HideWaitingPlayerBattleUi() =>
      _spectatorPlayerBattleUi.SetActive(false);

    private void InitializePlayerList()
    {
      if (_role == RoleState.ChosenFirst)
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        {
          var playerListElement =
            _instantiator.InstantiatePrefabForComponent<BattlePlayerListElement>(_playerListElementPrefab,
              _playerListScrollRect.content);
          playerListElement.Initialize(_characterRegistryNetwork.FirstPlayerCharacters[i]);
          Character character =
            _characterRegistry.CharactersById[_characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId];
          _battleLog.AddLog(
            $"Player <color=green>{character.Name}_{_characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId}</color>" +
            $" placed on {character.Position}");
        }
      else if (_role == RoleState.ChosenSecond)
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
        {
          var playerListElement =
            _instantiator.InstantiatePrefabForComponent<BattlePlayerListElement>(_playerListElementPrefab,
              _playerListScrollRect.content);
          playerListElement.Initialize(_characterRegistryNetwork.SecondPlayerCharacters[i]);
          Character character =
            _characterRegistry.CharactersById[_characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId];
          _battleLog.AddLog(
            $"Player <color=green>{character.Name}_{_characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId}</color>" +
            $" placed on {character.Position}");
        }
    }

    private void InitializeEnemyList()
    {
      if (_role == RoleState.ChosenFirst)
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
        {
          var enemyListElement =
            _instantiator.InstantiatePrefabForComponent<BattleEnemyListElement>(_enemyListElementPrefab,
              _enemyListScrollRect.content);
          enemyListElement.Initialize(_characterRegistryNetwork.SecondPlayerCharacters[i]);
          Enemy enemy = _characterRegistry.EnemiesById[_characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId];
          _battleLog.AddLog(
            $"Enemy <color=red>{enemy.Character.Name}_{_characterRegistryNetwork.SecondPlayerCharacters[i].InstanceId}</color>" +
            $" placed on {enemy.Character.Position}");
        }
      else if (_role == RoleState.ChosenSecond)
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        {
          var enemyListElement =
            _instantiator.InstantiatePrefabForComponent<BattleEnemyListElement>(_enemyListElementPrefab,
              _enemyListScrollRect.content);
          enemyListElement.Initialize(_characterRegistryNetwork.FirstPlayerCharacters[i]);
          Enemy enemy = _characterRegistry.EnemiesById[_characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId];
          _battleLog.AddLog(
            $"Enemy <color=red>{enemy.Character.Name}_{_characterRegistryNetwork.FirstPlayerCharacters[i].InstanceId}</color>" +
            $" placed on {enemy.Character.Position}");
        }
    }
  }
}