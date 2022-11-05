using System;
using ModestTree;
using UnityEngine;
using UnityEditor;
using Zenject;

namespace Zenject.MemoryPoolMonitor
{
	public class MpmWindow : ZenjectEditorWindow
	{
		[MenuItem("Window/Zenject Pool Monitor")]
		public static MpmWindow GetOrCreateWindow()
		{
			var window = GetWindow<MpmWindow>();
			window.titleContent = new GUIContent("Pool Monitor");
			return window;
		}

		public override void InstallBindings()
		{
			MpmSettingsInstaller.InstallFromResource(Container);

			Container.BindInstance(this);
			Container.BindInterfacesTo<MpmView>().AsSingle();
		}
	}
}