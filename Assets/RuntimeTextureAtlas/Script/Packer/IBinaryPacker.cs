using UnityEngine;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// 二维打包器接口
	/// </summary>
	public interface IBinaryPacker
	{
		/// <summary>
		/// 插入矩形
		/// </summary>
		RectInt Insert(int w, int h);

		/// <summary>
		/// 移除矩形
		/// </summary>
		void Remove(RectInt pos);
	}
}
