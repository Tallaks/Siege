using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities
{
	public class Pool<TElement> : IInitializable where TElement : Component
	{
		private readonly DiContainer _diContainer;
		private readonly Transform _poolContainer;
		private GameObject _prefab;
		private int _minCapacity;
		private int _maxCapacity = 100;
		private bool _autoExpand;
		private List<TElement> _pool = new();

		public Pool(DiContainer diContainer, GameObject prefab, int minCapacity, bool autoExpand = true)
		{
			_diContainer = diContainer;
			_prefab = prefab;
			_minCapacity = minCapacity;
			_autoExpand = autoExpand;

			if (_autoExpand)
			{
				_maxCapacity = Int32.MaxValue;
			}

			_poolContainer = new GameObject($"PoolContainer {typeof(TElement).Name}").transform;
		}

		public void Initialize() =>
			CreatePool();

		public TElement GetFreeElement(Vector3 position, Quaternion rotation)
		{
			TElement element = GetFreeElement();
			Transform transform = element.transform;
			transform.position = position;
			transform.rotation = rotation;
			return element;
		}

		private void CreatePool()
		{
			_pool = new List<TElement>(_minCapacity);

			for (var i = 0; i < _minCapacity; i++)
				CreateElement();
		}

		private TElement CreateElement(bool isActiveByDefault = false)
		{
			TElement createObject =
				_diContainer.InstantiatePrefabForComponent<TElement>(_prefab.GetComponent<TElement>(), _poolContainer);
			createObject.gameObject.SetActive(isActiveByDefault);

			_pool.Add(createObject);

			return createObject;
		}

		private bool TryGetElement(out TElement element)
		{
			int index = _pool.FindIndex(item => item.gameObject.activeInHierarchy == false);
			if (index != -1)
			{
				element = _pool[index];
				element.gameObject.SetActive(true);
				return true;
			}

			element = null;
			return false;
		}

		private TElement GetFreeElement()
		{
			if (TryGetElement(out TElement element))
				return element;

			if (_autoExpand || _pool.Count < _maxCapacity)
				return CreateElement(true);

			throw new Exception($"Pool of {typeof(TElement).Name} is Over!");
		}
	}
}