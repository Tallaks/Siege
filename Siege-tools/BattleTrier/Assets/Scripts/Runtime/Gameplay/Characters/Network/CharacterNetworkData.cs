using System;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network
{
  public struct CharacterNetworkData : INetworkSerializable, IEquatable<CharacterNetworkData>
  {
    public int TypeId;
    public RoleState PlayerRole;
    public int CurrentHp;
    public Vector2 TilePosition;

    public CharacterNetworkData(int id, RoleState playerRole, IStaticDataProvider configProvider)
    {
      TypeId = id;
      PlayerRole = playerRole;
      TilePosition = Vector2.positiveInfinity;
      CurrentHp = configProvider.ConfigById(id).HealthPoints;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref TypeId);
      serializer.SerializeValue(ref CurrentHp);
      serializer.SerializeValue(ref TilePosition);
      serializer.SerializeValue(ref PlayerRole);
    }

    public bool Equals(CharacterNetworkData other) =>
      other.TypeId == TypeId &&
      other.PlayerRole == PlayerRole &&
      other.TilePosition == TilePosition &&
      other.CurrentHp == CurrentHp;
  }
}