using UnityEngine;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// 使用贪婪算法的二维打包器
	/// </summary>
	public class GreedyPacker : IBinaryPacker
	{
		/// <summary>
		/// 矩形空间
		/// </summary>
		private Rectangle root;

		/// <summary>
		/// 构造函数
		/// </summary>
		public GreedyPacker(int w, int h)
		{
			root = new Column(0, 0, w, h);
		}

		/// <summary>
		/// 插入矩形
		/// </summary>
		public RectInt Insert(int w, int h)
		{
			var rect = root.Insert(w, h);
			if(rect == null)
				return new RectInt(-1, -1, -1, -1);

			return rect.position;
		}

		/// <summary>
		/// 移除矩形
		/// </summary>
		public void Remove(RectInt pos)
		{
			root.Remove(pos);
		}
	}
}