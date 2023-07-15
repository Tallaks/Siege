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
    public void ChangeCharacterPositionServerRpc(Vector2Int tilePosition, int characterId, RoleState changerRole)
    {
      if (FirstPlayerCharacters.Count + SecondPlayerCharacters.Count <= characterId)
      {
        Debug.LogError($"Character with id {characterId} not found");
        return;
      }

      for (var i = 0; i < FirstPlayerCharacters.Count; i++)
        if (changerRole == RoleState.ChosenFirst)
          if (FirstPlayerCharacters[i].InstanceId == characterId)
          {
            Debug.Log($"Changing server position of character {characterId} to {tilePosition}");
            FirstPlayerCharacters[i] = new CharacterNetworkData(
              FirstPlayerCharacters[i].TypeId,
              characterId,
              changerRole,
              tilePosition,
              _dataProvider);
            return;
            ;
          }

      for (var i = 0; i < SecondPlayerCharacters.Count; i++)
        if (changerRole == RoleState.ChosenSecond)
          if (SecondPlayerCharacters[i].InstanceId == characterId)
          {
            Debug.Log($"Changing server position of character {characterId} to {tilePosition}");
            SecondPlayerCharacters[i] = new CharacterNetworkData(
              SecondPlayerCharacters[i].TypeId,
              characterId,
              changerRole,
              tilePosition,
              _dataProvider);
            break;
          }
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