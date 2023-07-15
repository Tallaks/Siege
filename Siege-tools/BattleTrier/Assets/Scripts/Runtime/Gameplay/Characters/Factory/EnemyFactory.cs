using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Data;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Factory
{
  public class EnemyFactory : IEnemyFactory
  {
    private readonly DiContainer _diContainer;
    private readonly IStaticDataProvider _dataProvider;

    public EnemyFactory(DiContainer diContainer, IStaticDataProvider dataProvider)
    {
      _diContainer = diContainer;
      _dataProvider = dataProvider;
    }

    public Enemy Create(int configId)
    {
      CharacterConfig config = _dataProvider.ConfigById(configId);
      var enemy = _diContainer.InstantiateComponent<Enemy>(
        _diContainer.InstantiatePrefabForComponent<Character>(config.Prefab).gameObject);
      var character = enemy.GetComponent<Character>();
      character.name = config.Name;
      character.Renderer.sprite = config.Icon;
      return enemy;
    }
  }
}