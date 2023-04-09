using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public class CharacterFactory : ICharacterFactory
  {
    private DiContainer _container;

    public CharacterFactory(DiContainer container) =>
      _container = container;

    public Character Create(CharacterConfig config)
    {
      var instantiatePrefabForComponent = _container.InstantiatePrefabForComponent<Character>(config.Prefab);
      instantiatePrefabForComponent.name = config.Name;
      instantiatePrefabForComponent.Renderer.sprite = config.Icon;
      return instantiatePrefabForComponent;
    }
  }
}