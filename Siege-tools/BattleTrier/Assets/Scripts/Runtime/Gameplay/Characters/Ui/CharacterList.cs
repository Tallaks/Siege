using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Data;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterList : MonoBehaviour
  {
    [SerializeField, Required, AssetSelector] private CharacterListItem _characterListItemPrefab;
    [SerializeField, Required, SceneObjectsOnly] private Transform _listContainer;

    private readonly List<CharacterListItem> _characterList = new();

    private ICharacterRegistry _characterRegistry;
    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container, ICharacterRegistry characterRegistry)
    {
      _container = container;
      _characterRegistry = characterRegistry;
    }

    public void ChangeCharacterList()
    {
      foreach (KeyValuePair<CharacterConfig, int> characterGroup in _characterRegistry.Characters)
      {
        for (var index = 0; index < _characterList.Count; index++)
        {
          if (!_characterRegistry.Characters.ContainsKey(_characterList[index].Config))
          {
            Destroy(_characterList[index].gameObject);
            _characterList.Remove(_characterList[index]);
            continue;
          }

          if (_characterList[index].Config == characterGroup.Key)
            _characterList[index].SetAmount(characterGroup.Value);
        }

        if (!_characterList.Select(k => k.Config).Contains(characterGroup.Key))
        {
          var listItem = _container.InstantiatePrefabForComponent<CharacterListItem>(_characterListItemPrefab, _listContainer);
          listItem.Initialize(characterGroup.Key);
          _characterList.Add(listItem);
        }
      }
    }
  }
}