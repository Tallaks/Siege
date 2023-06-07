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

    public Character Create(int id, Vector2Int position)
    {
      CharacterConfig config = _dataProvider.ConfigById(id);
      var character = _container.InstantiatePrefabForComponent<Character>(config.Prefab,
        new Vector3(-4f, -1.5f, 0) + new Vector3(position.x, position.y) * 0.7f, Quaternion.identity,
        null);
      Debug.Log($"Instantiated {config.Name}", character);
      character.name = config.Name;
      character.Renderer.sprite = config.Icon;
      character.Position = position;
      return character;
    }
  }
}