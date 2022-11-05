using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
	[NoReflectionBaking]
	public class ScopableBindingFinalizer : ProviderBindingFinalizer
	{
		private readonly Func<DiContainer, Type, IProvider> _providerFactory;

		public ScopableBindingFinalizer(
			BindInfo bindInfo, Func<DiContainer, Type, IProvider> providerFactory)
			: base(bindInfo)
		{
			_providerFactory = providerFactory;
		}

		protected override void OnFinalizeBinding(DiContainer container)
		{
			if (BindInfo.ToChoice == ToChoices.Self)
			{
				Assert.IsEmpty(BindInfo.ToTypes);
				FinalizeBindingSelf(container);
			}
			else
			{
				FinalizeBindingConcrete(container, BindInfo.ToTypes);
			}
		}

		private void FinalizeBindingConcrete(DiContainer container, List<Type> concreteTypes)
		{
			if (concreteTypes.Count == 0)
				// This can be common when using convention based bindings
				return;

			ScopeTypes scope = GetScope();
			switch (scope)
			{
				case ScopeTypes.Transient:
				{
					RegisterProvidersForAllContractsPerConcreteType(
						container, concreteTypes, _providerFactory);
					break;
				}
				case ScopeTypes.Singleton:
				{
					RegisterProvidersForAllContractsPerConcreteType(
						container,
						concreteTypes,
						(_, concreteType) =>
							BindingUtil.CreateCachedProvider(
								_providerFactory(container, concreteType)));
					break;
				}
				default:
				{
					throw Assert.CreateException();
				}
			}
		}

		private void FinalizeBindingSelf(DiContainer container)
		{
			ScopeTypes scope = GetScope();

			switch (scope)
			{
				case ScopeTypes.Transient:
				{
					RegisterProviderPerContract(container, _providerFactory);
					break;
				}
				case ScopeTypes.Singleton:
				{
					RegisterProviderPerContract(
						container,
						(_, contractType) =>
							BindingUtil.CreateCachedProvider(
								_providerFactory(container, contractType)));
					break;
				}
				default:
				{
					throw Assert.CreateException();
				}
			}
		}
	}
}