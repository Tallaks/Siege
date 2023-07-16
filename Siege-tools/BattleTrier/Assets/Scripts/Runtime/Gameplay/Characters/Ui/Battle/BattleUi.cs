using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui.Battle
{
  public class BattleUi : MonoBehaviour
  {
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

    private CharacterRegistryNetwork _characterRegistryNetwork;
    private IInstantiator _instantiator;

    private RoleBase _roleBase;

    [Inject]
    private void Construct(IInstantiator instantiator, CharacterRegistryNetwork characterRegistryNetwork,
      RoleBase roleBase)
    {
      _instantiator = instantiator;
      _characterRegistryNetwork = characterRegistryNetwork;
      _roleBase = roleBase;
    }

    public void ShowActivePlayerBattleUi()
    {
      _activePlayerBattleUi.SetActive(true);
      InitializePlayerList();
      InitializeEnemyList();
    }

    public void ShowSpectatorBattleUi() =>
      _spectatorPlayerBattleUi.SetActive(true);

    public void HideActivePlayerBattleUi() =>
      _activePlayerBattleUi.SetActive(false);

    public void HideWaitingPlayerBattleUi() =>
      _spectatorPlayerBattleUi.SetActive(false);

    private void InitializeEnemyList()
    {
      if (_roleBase.State.Value == RoleState.ChosenFirst)
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
        {
          var enemyListElement =
            _instantiator.InstantiatePrefabForComponent<BattleEnemyListElement>(_enemyListElementPrefab,
              _enemyListScrollRect.content);
          enemyListElement.Initialize(_characterRegistryNetwork.SecondPlayerCharacters[i]);
        }
      else if (_roleBase.State.Value == RoleState.ChosenSecond)
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        {
          var enemyListElement =
            _instantiator.InstantiatePrefabForComponent<BattleEnemyListElement>(_enemyListElementPrefab,
              _enemyListScrollRect.content);
          enemyListElement.Initialize(_characterRegistryNetwork.FirstPlayerCharacters[i]);
        }
    }

    private void InitializePlayerList()
    {
      if (_roleBase.State.Value == RoleState.ChosenFirst)
        for (var i = 0; i < _characterRegistryNetwork.FirstPlayerCharacters.Count; i++)
        {
          var playerListElement =
            _instantiator.InstantiatePrefabForComponent<BattlePlayerListElement>(_playerListElementPrefab,
              _playerListScrollRect.content);
          playerListElement.Initialize(_characterRegistryNetwork.FirstPlayerCharacters[i]);
        }
      else if (_roleBase.State.Value == RoleState.ChosenSecond)
        for (var i = 0; i < _characterRegistryNetwork.SecondPlayerCharacters.Count; i++)
        {
          var playerListElement =
            _instantiator.InstantiatePrefabForComponent<BattlePlayerListElement>(_playerListElementPrefab,
              _playerListScrollRect.content);
          playerListElement.Initialize(_characterRegistryNetwork.SecondPlayerCharacters[i]);
        }
    }
  }
}