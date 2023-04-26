using System;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network
{
  public class CharacterRegistryNetwork : NetworkBehaviour
  {
    public NetworkList<CharacterNetworkData> FirstPlayerCharacters;
    public NetworkList<CharacterNetworkData> SecondPlayerCharacters;

    private IStaticDataProvider _dataProvider;
    private RoleBase _playerRole;

    [Inject]
    private void Construct(IStaticDataProvider dataProvider) =>
      _dataProvider = dataProvider;

    private void Awake()
    {
      FirstPlayerCharacters = new();
      SecondPlayerCharacters = new();
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      FirstPlayerCharacters?.Dispose();
      SecondPlayerCharacters?.Dispose();
    }

    [ServerRpc(RequireOwnership = false)]
    public void RegisterByIdServerRpc(int characterId, RoleState role)
    {
      var networkData = new CharacterNetworkData(characterId, role, _dataProvider);
      if (role == RoleState.ChosenFirst)
      {
        Debug.Log($"Added character with {characterId} for first player");
        FirstPlayerCharacters.Add(networkData);
      }
      else
      {
        Debug.Log($"Added character with {characterId} for second player");
        SecondPlayerCharacters.Add(networkData);
      }
    }
  }
}