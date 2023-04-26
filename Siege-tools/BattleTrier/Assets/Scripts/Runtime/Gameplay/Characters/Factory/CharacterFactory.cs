using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Network;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Network.Roles;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public class CharacterFactory : ICharacterFactory
  {
    private readonly IStaticDataProvider _dataProvider;
    private readonly DiContainer _container;

    public CharacterFactory(IStaticDataProvider dataProvider, DiContainer container)
    {
      _dataProvider = dataProvider;
      _container = container;
    }

    public Character Create(CharacterNetworkData networkData)
    {
      CharacterConfig config = _dataProvider.ConfigById(networkData.TypeId);
      Character instantiatePrefabForComponent;
      if(networkData.PlayerRole == RoleState.ChosenFirst)
        instantiatePrefabForComponent = 
          _container.InstantiatePrefabForComponent<Character>(
            config.Prefab,
            new Vector3(-5, 0),
            Quaternion.identity,
            null);
      else
      {
        instantiatePrefabForComponent =
          _container.InstantiatePrefabForComponent<Character>(
            config.Prefab,
            new Vector3(5, 0),
            Quaternion.identity,
            null);
      }
      Debug.Log($"Instantiated {config.Name}", instantiatePrefabForComponent);
      instantiatePrefabForComponent.name = config.Name;
      instantiatePrefabForComponent.Renderer.sprite = config.Icon;
      return instantiatePrefabForComponent;
    }
  }
}