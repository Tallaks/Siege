using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
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

    public Character Create(int configId)
    {
      CharacterConfig config = _dataProvider.ConfigById(configId);
      var instantiatePrefabForComponent = _container.InstantiatePrefabForComponent<Character>(config.Prefab);
      instantiatePrefabForComponent.name = config.Name;
      instantiatePrefabForComponent.Renderer.sprite = config.Icon;
      return instantiatePrefabForComponent;
    }
  }
}