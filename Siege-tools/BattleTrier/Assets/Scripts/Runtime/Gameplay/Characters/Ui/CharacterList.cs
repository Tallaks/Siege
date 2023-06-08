using System.Collections.Generic;
using System.Linq;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Registry;
using Kulinaria.Tools.BattleTrier.Runtime.Gameplay.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.Characters.Ui
{
  public class CharacterList : MonoBehaviour
  {
    private readonly List<CharacterListItem> _characterList = new();
    [SerializeField, Required, AssetSelector] private CharacterListItem _characterListItemPrefab;
    [SerializeField, Required, SceneObjectsOnly] private Transform _listContainer;

    private ICharacterRegistry _characterRegistry;
    private DiContainer _container;
    private GameplayMediator _mediator;

    [Inject]
    private void Construct(DiContainer container, ICharacterRegistry characterRegistry, GameplayMediator mediator)
    {
      _container = container;
      _characterRegistry = characterRegistry;
      _mediator = mediator;
    }

    public void ChangeCharacterList()
    {
      foreach (KeyValuePair<int, int> characterGroup in _characterRegistry.Characters)
      {
        for (var index = 0; index < _characterList.Count; index++)
        {
          if (!_characterRegistry.Characters.ContainsKey(_characterList[index].Config.Id))
          {
            Destroy(_characterList[index].gameObject);
            _characterList.Remove(_characterList[index]);
            continue;
          }

          if (_characterList[index].Config.Id == characterGroup.Key)
            _characterList[index].SetAmount(characterGroup.Value);
        }

        if (!_characterList.Select(k => k.Config.Id).Contains(characterGroup.Key))
        {
          var listItem =
            _container.InstantiatePrefabForComponent<CharacterListItem>(_characterListItemPrefab, _listContainer);
          listItem.Initialize(characterGroup.Key);
          _characterList.Add(listItem);
        }
      }

      if (_characterRegistry.Characters.Count == 0)
        _mediator.DisableCharacterSelectSubmitButton();
      else
        _mediator.EnableCharacterSelectSubmitButton();
    }
  }
}