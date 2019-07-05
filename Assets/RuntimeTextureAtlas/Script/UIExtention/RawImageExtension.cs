using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// RawImage方法扩展.
	/// </summary>
	public static class RawImageExtension
	{
		/// <summary>
		/// 将贴图打包到图集，并应用到RawImage控件
		/// </summary>
		public static void PackTexture(this RawImage image, Texture texture, Action onSuccess = null)
		{
			// 由RawImageAgent完成实际功能
			var agent = image.GetComponent<RawImageAgent>() ?? image.gameObject.AddComponent<RawImageAgent>();
			agent.PackTexture(texture, onSuccess);
		}
	}
}
