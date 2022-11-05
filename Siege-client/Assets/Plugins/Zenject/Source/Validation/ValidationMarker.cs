using System;

namespace Zenject
{
	[NoReflectionBaking]
	public class ValidationMarker
	{
		public ValidationMarker(
			Type markedType, bool instantiateFailed)
		{
			MarkedType = markedType;
			InstantiateFailed = instantiateFailed;
		}

		public ValidationMarker(Type markedType)
			: this(markedType, false)
		{
		}

		public bool InstantiateFailed { get; }

		public Type MarkedType { get; }
	}
}