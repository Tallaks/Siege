using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;
using Zenject;

namespace Zenject.MemoryPoolMonitor
{
	public class MpmView : IGuiRenderable, ITickable, IInitializable
	{
		private const int NumColumns = 6;

		private static readonly string[] ColumnTitles =
		{
			"Pool Type", "Num Total", "Num Active", "Num Inactive", "", ""
		};

		private readonly List<IMemoryPool> _pools = new();
		private readonly Settings _settings;
		private readonly MpmWindow _window;
		private string _actualFilter = "";

		private int _controlID;
		private Texture2D _lineTexture;
		private bool _poolListDirty;
		private Texture2D _rowBackground1;
		private Texture2D _rowBackground2;
		private Texture2D _rowBackgroundHighlighted;
		private Texture2D _rowBackgroundSelected;
		private float _scrollPosition;
		private string _searchFilter = "";
		private Type _selectedPoolType;
		private int _sortColumn;
		private bool _sortDescending;

		public MpmView(
			MpmWindow window,
			Settings settings)
		{
			_settings = settings;
			_window = window;
		}

		public float HeaderTop => _settings.HeaderHeight + _settings.FilterHeight;

		public float TotalWidth => _window.position.width;

		public float TotalHeight => _window.position.height;

		private Texture2D RowBackground1
		{
			get
			{
				if (_rowBackground1 == null) _rowBackground1 = CreateColorTexture(_settings.RowBackground1);

				return _rowBackground1;
			}
		}

		private Texture2D RowBackground2
		{
			get
			{
				if (_rowBackground2 == null) _rowBackground2 = CreateColorTexture(_settings.RowBackground2);

				return _rowBackground2;
			}
		}

		private Texture2D RowBackgroundHighlighted
		{
			get
			{
				if (_rowBackgroundHighlighted == null)
					_rowBackgroundHighlighted = CreateColorTexture(_settings.RowBackgroundHighlighted);

				return _rowBackgroundHighlighted;
			}
		}

		private Texture2D RowBackgroundSelected
		{
			get
			{
				if (_rowBackgroundSelected == null)
					_rowBackgroundSelected = CreateColorTexture(_settings.RowBackgroundSelected);

				return _rowBackgroundSelected;
			}
		}

		private Texture2D LineTexture
		{
			get
			{
				if (_lineTexture == null) _lineTexture = CreateColorTexture(_settings.LineColor);

				return _lineTexture;
			}
		}

		public void GuiRender()
		{
			_controlID = GUIUtility.GetControlID(FocusType.Passive);

			var windowBounds = new Rect(0, 0, TotalWidth, _window.position.height);

			var scrollbarSize = new Vector2(
				GUI.skin.horizontalScrollbar.CalcSize(GUIContent.none).y,
				GUI.skin.verticalScrollbar.CalcSize(GUIContent.none).x);

			GUI.Label(new Rect(
				0, 0, _settings.FilterPaddingLeft, _settings.FilterHeight), "Filter:", _settings.FilterTextStyle);

			string searchFilter = GUI.TextField(
				new Rect(_settings.FilterPaddingLeft, _settings.FilterPaddingTop, _settings.FilterWidth,
					_settings.FilterInputHeight), _searchFilter, 999);

			if (searchFilter != _searchFilter)
			{
				_searchFilter = searchFilter;
				_actualFilter = _searchFilter.Trim().ToLowerInvariant();
				_poolListDirty = true;
			}

			var viewArea = new Rect(0, HeaderTop, TotalWidth - scrollbarSize.y, _window.position.height - HeaderTop);

			var contentRect = new Rect(
				0, 0, viewArea.width, _pools.Count() * _settings.RowHeight);

			var vScrRect = new Rect(
				windowBounds.x + viewArea.width, HeaderTop, scrollbarSize.y, viewArea.height);

			_scrollPosition = GUI.VerticalScrollbar(
				vScrRect, _scrollPosition, viewArea.height, 0, contentRect.height);

			DrawColumnHeaders(viewArea.width);

			GUI.BeginGroup(viewArea);
			{
				contentRect.y = -_scrollPosition;

				GUI.BeginGroup(contentRect);
				{
					DrawContent(contentRect.width);
				}
				GUI.EndGroup();
			}
			GUI.EndGroup();

			HandleEvents();
		}

		public void Initialize()
		{
			StaticMemoryPoolRegistry.PoolAdded += OnPoolListChanged;
			StaticMemoryPoolRegistry.PoolRemoved += OnPoolListChanged;
			_poolListDirty = true;
		}

		public void Tick()
		{
			if (_poolListDirty)
			{
				_poolListDirty = false;

				_pools.Clear();
				_pools.AddRange(StaticMemoryPoolRegistry.Pools.Where(ShouldIncludePool));
			}

			InPlaceStableSort<IMemoryPool>.Sort(_pools, ComparePools);
		}

		private string GetName(IMemoryPool pool)
		{
			Type type = pool.GetType();
			return "{0}.{1}".Fmt(type.Namespace, type.PrettyName());
		}

		private Texture2D CreateColorTexture(Color color)
		{
			var texture = new Texture2D(1, 1);
			texture.SetPixel(1, 1, color);
			texture.Apply();
			return texture;
		}

		private void OnPoolListChanged(IMemoryPool pool)
		{
			_poolListDirty = true;
		}

		private bool ShouldIncludePool(IMemoryPool pool)
		{
			//var poolType = pool.GetType();

			//if (poolType.Namespace == "Zenject")
			//{
			//return false;
			//}

			if (_actualFilter.IsEmpty()) return true;

			return GetName(pool).ToLowerInvariant().Contains(_actualFilter);
		}

		private void DrawColumnHeaders(float width)
		{
			GUI.DrawTexture(new Rect(
				0, _settings.FilterHeight - 0.5f * _settings.SplitterWidth, width, _settings.SplitterWidth), LineTexture);

			GUI.DrawTexture(new Rect(
				0, HeaderTop - 0.5f * _settings.SplitterWidth, width, _settings.SplitterWidth), LineTexture);

			var columnPos = 0.0f;

			for (var i = 0; i < NumColumns; i++)
			{
				float columnWidth = GetColumnWidth(i);
				DrawColumn1(i, columnPos, columnWidth);
				columnPos += columnWidth;
			}
		}

		private void DrawColumn1(
			int index, float position, float width)
		{
			float columnHeight = _settings.HeaderHeight + _pools.Count() * _settings.RowHeight;

			if (index < 4)
				GUI.DrawTexture(new Rect(
					position + width - _settings.SplitterWidth * 0.5f, _settings.FilterHeight,
					_settings.SplitterWidth, columnHeight), LineTexture);

			var headerBounds = new Rect(
				position + 0.5f * _settings.SplitterWidth,
				_settings.FilterHeight,
				width - _settings.SplitterWidth, _settings.HeaderHeight);

			DrawColumnHeader(index, headerBounds, ColumnTitles[index]);
		}

		private void HandleEvents()
		{
			switch (Event.current.GetTypeForControl(_controlID))
			{
				case EventType.ScrollWheel:
				{
					_scrollPosition =
						Mathf.Clamp(_scrollPosition + Event.current.delta.y * _settings.ScrollSpeed, 0, TotalHeight);
					break;
				}
				case EventType.MouseDown:
				{
					_selectedPoolType = TryGetPoolTypeUnderMouse();
					break;
				}
			}
		}

		private Type TryGetPoolTypeUnderMouse()
		{
			Vector2 mousePositionInContent = Event.current.mousePosition + Vector2.up * _scrollPosition;

			for (var i = 0; i < _pools.Count; i++)
			{
				IMemoryPool pool = _pools[i];

				Rect rowRect = GetPoolRowRect(i);
				rowRect.y += HeaderTop;

				if (rowRect.Contains(mousePositionInContent)) return pool.GetType();
			}

			return null;
		}

		private Rect GetPoolRowRect(int index)
		{
			return new Rect(
				0, index * _settings.RowHeight, TotalWidth, _settings.RowHeight);
		}

		private void DrawRowBackgrounds()
		{
			Vector2 mousePositionInContent = Event.current.mousePosition;

			for (var i = 0; i < _pools.Count; i++)
			{
				IMemoryPool pool = _pools[i];
				Rect rowRect = GetPoolRowRect(i);

				Texture2D background;

				if (pool.GetType() == _selectedPoolType)
				{
					background = RowBackgroundSelected;
				}
				else
				{
					if (rowRect.Contains(mousePositionInContent))
						background = RowBackgroundHighlighted;
					else if (i % 2 == 0)
						background = RowBackground1;
					else
						background = RowBackground2;
				}

				GUI.DrawTexture(rowRect, background);
			}
		}

		private float GetColumnWidth(int index)
		{
			if (index == 0) return TotalWidth - (NumColumns - 1) * _settings.NormalColumnWidth;

			return _settings.NormalColumnWidth;
		}

		private void DrawContent(float width)
		{
			DrawRowBackgrounds();

			var columnPos = 0.0f;

			for (var i = 0; i < NumColumns; i++)
			{
				float columnWidth = GetColumnWidth(i);
				DrawColumn(i, columnPos, columnWidth);
				columnPos += columnWidth;
			}
		}

		private void DrawColumn(
			int index, float position, float width)
		{
			float columnHeight = _settings.HeaderHeight + _pools.Count() * _settings.RowHeight;

			if (index < 4)
				GUI.DrawTexture(new Rect(
					position + width - _settings.SplitterWidth * 0.5f, 0,
					_settings.SplitterWidth, columnHeight), LineTexture);

			var columnBounds = new Rect(
				position + 0.5f * _settings.SplitterWidth, 0, width - _settings.SplitterWidth, columnHeight);

			GUI.BeginGroup(columnBounds);
			{
				for (var i = 0; i < _pools.Count; i++)
				{
					IMemoryPool pool = _pools[i];

					var cellBounds = new Rect(
						0, _settings.RowHeight * i,
						columnBounds.width, _settings.RowHeight);

					DrawColumnContents(index, cellBounds, pool);
				}
			}
			GUI.EndGroup();
		}

		private void DrawColumnContents(
			int index, Rect bounds, IMemoryPool pool)
		{
			switch (index)
			{
				case 0:
				{
					GUI.Label(bounds, GetName(pool), _settings.ContentNameTextStyle);
					break;
				}
				case 1:
				{
					GUI.Label(bounds, pool.NumTotal.ToString(), _settings.ContentNumberTextStyle);
					break;
				}
				case 2:
				{
					GUI.Label(bounds, pool.NumActive.ToString(), _settings.ContentNumberTextStyle);
					break;
				}
				case 3:
				{
					GUI.Label(bounds, pool.NumInactive.ToString(), _settings.ContentNumberTextStyle);
					break;
				}
				case 4:
				{
					var buttonBounds = new Rect(
						bounds.x + _settings.ButtonMargin, bounds.y, bounds.width - _settings.ButtonMargin, bounds.height);

					if (GUI.Button(buttonBounds, "Clear")) pool.Clear();
					break;
				}
				case 5:
				{
					var buttonBounds = new Rect(
						bounds.x, bounds.y, bounds.width - 15.0f, bounds.height);

					if (GUI.Button(buttonBounds, "Expand")) pool.ExpandBy(5);
					break;
				}
				default:
				{
					throw Assert.CreateException();
				}
			}
		}

		private void DrawColumnHeader(int index, Rect bounds, string text)
		{
			if (index > 3) return;

			if (_sortColumn == index)
			{
				Vector2 offset = _settings.TriangleOffset;
				Texture2D image = _sortDescending ? _settings.TriangleDown : _settings.TriangleUp;

				GUI.DrawTexture(new Rect(bounds.x + offset.x, bounds.y + offset.y, image.width, image.height), image);
			}

			if (GUI.Button(bounds, text, index == 0 ? _settings.HeaderTextStyleName : _settings.HeaderTextStyle))
			{
				if (_sortColumn == index)
					_sortDescending = !_sortDescending;
				else
					_sortColumn = index;
			}
		}

		private int ComparePools(IMemoryPool left, IMemoryPool right)
		{
			if (_sortDescending)
			{
				IMemoryPool temp = right;
				right = left;
				left = temp;
			}

			switch (_sortColumn)
			{
				case 4:
				case 5:
				case 0:
				{
					return GetName(left).CompareTo(GetName(right));
				}
				case 1:
				{
					return left.NumTotal.CompareTo(right.NumTotal);
				}
				case 2:
				{
					return left.NumActive.CompareTo(right.NumActive);
				}
				case 3:
				{
					return left.NumInactive.CompareTo(right.NumInactive);
				}
			}

			throw Assert.CreateException();
		}

		[Serializable]
		public class Settings
		{
			public Texture2D TriangleUp;
			public Texture2D TriangleDown;
			public Vector2 TriangleOffset;

			public GUIStyle FilterTextStyle;
			public GUIStyle HeaderTextStyleName;
			public GUIStyle HeaderTextStyle;
			public GUIStyle ContentNumberTextStyle;
			public GUIStyle ContentNameTextStyle;

			public Color RowBackground1;
			public Color RowBackground2;
			public Color RowBackgroundHighlighted;
			public Color RowBackgroundSelected;
			public Color LineColor;

			public float ScrollSpeed = 1.5f;
			public float NormalColumnWidth;
			public float HeaderHeight;
			public float FilterHeight;
			public float FilterInputHeight;
			public float FilterWidth;
			public float FilterPaddingLeft;
			public float FilterPaddingTop = 10;

			public float SplitterWidth;
			public float RowHeight;

			public float ButtonMargin = 3;
		}
	}
}