using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// 矩形
	/// </summary>
	public abstract class Rectangle
	{
		/// <summary>
		/// 矩形坐标
		/// </summary>
		public RectInt position;

		/// <summary>
		/// 是否已经被占用
		/// </summary>
		public bool used;

		/// <summary>
		/// 构造函数
		/// </summary>
		public Rectangle(int x, int y, int w, int h)
		{
			position = new RectInt(x, y, w, h);
		}

		/// <summary>
		/// 判断矩形是否包含
		/// </summary>
		public bool Contains(RectInt rect)
		{
            return (position.x <= rect.x && position.y <= rect.y && position.xMax >= rect.xMax && position.yMax >= rect.yMax);
		}

		/// <summary>
		/// 插入矩形
		/// </summary>
		public abstract Rectangle Insert(int w, int h);

		/// <summary>
		/// 移除矩形
		/// </summary>
		public abstract bool Remove(RectInt rect);
	}
}
