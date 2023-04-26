using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network
{
  public class CharacterRegistryNetwork : NetworkBehaviour, IInitializable
  {
    private readonly NetworkList<CharacterNetworkData> _firstPlayerCharacters = new();
    private readonly NetworkList<CharacterNetworkData> _secondPlayerCharacters = new();

    private IStaticDataProvider _dataProvider;
    private RoleBase _playerRole;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider) =>
      _dataProvider = dataProvider;

    public void Initialize()
    {
      _firstPlayerCharacters.OnListChanged += OnFirstListChanged;
      _secondPlayerCharacters.OnListChanged += OnSecondListChanged;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RegisterByIdServerRpc(int characterId, RoleState role)
    {
      var networkData = new CharacterNetworkData(characterId, role, _dataProvider);
      if(role == RoleState.ChosenFirst)
        _firstPlayerCharacters.Add(networkData);
      else
        _secondPlayerCharacters.Add(networkData);
    }

    private void OnFirstListChanged(NetworkListEvent<CharacterNetworkData> changeEvent) =>
      Debug.Log("Character with id " + changeEvent.Value.TypeId + " registered as first player char");

    private void OnSecondListChanged(NetworkListEvent<CharacterNetworkData> changeEvent) =>
      Debug.Log("Character with id " + changeEvent.Value.TypeId + " registered as second player char");
  }
}