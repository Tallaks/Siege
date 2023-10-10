using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Utilities
{
	public class Pool<TElement> : IInitializable where TElement : Component, new()
	{
		private readonly IInstantiator _instantiator;
		private readonly Transform _poolContainer;
		private readonly GameObject _prefab;
		private IObjectPool<TElement> _pool;

		public Pool(IInstantiator instantiator, GameObject prefab)
		{
			_instantiator = instantiator;
			_poolContainer = new GameObject($"PoolContainer {typeof(TElement).Name}").transform;
			_prefab = prefab;
		}

		public void Initialize()
		{
			CreatePool();
		}

		public TElement Get()
		{
			return _pool.Get();
		}

		public void Release(TElement element)
		{
			_pool.Release(element);
		}

		private void CreatePool()
		{
			_pool = new ObjectPool<TElement>(CreateElement, GetFreeElement, ReleaseElement, OnDestroyElement);
		}

		private void OnDestroyElement(TElement element)
		{
			Object.Destroy(element.gameObject);
		}

		private void ReleaseElement(TElement element)
		{
			element.gameObject.SetActive(false);
		}

		private void GetFreeElement(TElement element)
		{
			element.gameObject.SetActive(true);
		}

		private TElement CreateElement()
		{
			return _instantiator.InstantiatePrefabForComponent<TElement>(_prefab.GetComponent<TElement>(), _poolContainer);
		}
	}
}