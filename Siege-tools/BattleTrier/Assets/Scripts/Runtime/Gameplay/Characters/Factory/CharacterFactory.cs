using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public class CharacterFactory : ICharacterFactory
  {
    private readonly DiContainer _container;
    private readonly IStaticDataProvider _dataProvider;

    public CharacterFactory(IStaticDataProvider dataProvider, DiContainer container)
    {
      _dataProvider = dataProvider;
      _container = container;
    }

    public Character Create(int id)
    {
      CharacterConfig config = _dataProvider.ConfigById(id);
      var character = _container.InstantiatePrefabForComponent<Character>(config.Prefab);
      Debug.Log($"Instantiated {config.Name}", character);
      character.name = config.Name;
      character.Id = id;
      character.Renderer.sprite = config.Icon;
      return character;
    }
  }
}