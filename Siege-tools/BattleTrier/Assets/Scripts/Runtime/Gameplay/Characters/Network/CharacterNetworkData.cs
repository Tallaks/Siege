using System;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network
{
  public struct CharacterNetworkData : INetworkSerializable, IEquatable<CharacterNetworkData>
  {
    public int CurrentAp;
    public int CurrentHp;
    public int InstanceId;
    public RoleState PlayerRole;
    public Vector2 TilePosition;
    public int TypeId;

    public CharacterNetworkData(int id, int instanceId, RoleState playerRole, IStaticDataProvider configProvider)
    {
      TypeId = id;
      InstanceId = instanceId;
      PlayerRole = playerRole;
      TilePosition = new Vector2(-100, -100);
      CurrentHp = configProvider.ConfigById(id).HealthPoints;
      CurrentAp = configProvider.ConfigById(id).ActionPoints;
    }

    public CharacterNetworkData(int id, int instanceId, RoleState playerRole, Vector2Int position,
      IStaticDataProvider configProvider)
    {
      TypeId = id;
      InstanceId = instanceId;
      PlayerRole = playerRole;
      TilePosition = position;
      CurrentHp = configProvider.ConfigById(id).HealthPoints;
      CurrentAp = configProvider.ConfigById(id).ActionPoints;
    }

    public bool Equals(CharacterNetworkData other) =>
      other.TypeId == TypeId &&
      other.PlayerRole == PlayerRole &&
      other.TilePosition == TilePosition &&
      other.CurrentHp == CurrentHp &&
      other.CurrentAp == CurrentAp &&
      other.InstanceId == InstanceId;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
      serializer.SerializeValue(ref TypeId);
      serializer.SerializeValue(ref CurrentHp);
      serializer.SerializeValue(ref TilePosition);
      serializer.SerializeValue(ref PlayerRole);
      serializer.SerializeValue(ref InstanceId);
      serializer.SerializeValue(ref CurrentAp);
    }
  }
}