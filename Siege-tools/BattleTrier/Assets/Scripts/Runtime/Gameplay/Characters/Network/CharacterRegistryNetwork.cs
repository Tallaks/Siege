using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network
{
  public class CharacterRegistryNetwork : NetworkBehaviour
  {
    private IStaticDataProvider _dataProvider;
    private RoleBase _playerRole;
    public NetworkList<CharacterNetworkData> FirstPlayerCharacters;
    public NetworkList<CharacterNetworkData> SecondPlayerCharacters;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider) =>
      _dataProvider = dataProvider;

    private void Awake()
    {
      FirstPlayerCharacters = new NetworkList<CharacterNetworkData>();
      SecondPlayerCharacters = new NetworkList<CharacterNetworkData>();
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      FirstPlayerCharacters?.Dispose();
      SecondPlayerCharacters?.Dispose();
    }

    [ServerRpc(RequireOwnership = false)]
    public void RegisterByIdServerRpc(int characterTypeId, RoleState role)
    {
      int characterId = FirstPlayerCharacters.Count + SecondPlayerCharacters.Count;
      var networkData = new CharacterNetworkData(characterTypeId, characterId, role, _dataProvider);
      if (role == RoleState.ChosenFirst)
      {
        Debug.Log($"Added character {characterId} with config id {characterTypeId} for first player");
        FirstPlayerCharacters.Add(networkData);
      }
      else
      {
        Debug.Log($"Added character {characterId} with config id {characterTypeId} for second player");
        SecondPlayerCharacters.Add(networkData);
      }
    }
  }
}