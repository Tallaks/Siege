using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public class EnemyFactory : IEnemyFactory
  {
    private readonly DiContainer _diContainer;
    private readonly IStaticDataProvider _dataProvider;
    private readonly ICharacterRegistry _registry;

    public EnemyFactory(DiContainer diContainer, IStaticDataProvider dataProvider, ICharacterRegistry registry)
    {
      _diContainer = diContainer;
      _dataProvider = dataProvider;
      _registry = registry;
    }

    public Enemy Create(int configId)
    {
      CharacterConfig config = _dataProvider.ConfigById(configId);
      var enemy = _diContainer.InstantiateComponent<Enemy>(
        _diContainer.InstantiatePrefabForComponent<Character>(config.Prefab).gameObject);
      var character = enemy.GetComponent<Character>();
      character.name = config.Name;
      character.Renderer.sprite = config.Icon;
      _registry.Register(character);
      return enemy;
    }
  }
}