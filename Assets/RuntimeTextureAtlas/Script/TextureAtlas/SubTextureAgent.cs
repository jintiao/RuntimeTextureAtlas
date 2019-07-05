using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// 图集子贴图代理
	/// </summary>
	public abstract class SubTextureAgent : MonoBehaviour
	{
		/// <summary>
		/// 所属图集对象
		/// </summary>
		public TextureAtlas textureAtlas { get; private set; }

		/// <summary>
		/// 子贴图坐标数据
		/// </summary>
		public RectInt position { get; private set; }

		/// <summary>
		/// 获取子贴图期望尺寸
		/// </summary>
		protected abstract Vector2Int GetSize();

		/// <summary>
		/// 打包前的回调函数
		/// </summary>
		protected virtual void BeforePackTexture(Texture texture) { }

		/// <summary>
		/// 打包后的回调函数
		/// </summary>
		protected virtual void AfterPackTexture(Texture texture) { }

		/// <summary>
		/// 将贴图打包进图集.
		/// </summary>
		public void PackTexture(Texture texture, Action onSuccess = null)
		{
			// 当texture为空，则销毁子贴图代理
			if(texture == null)
			{
				Destroy(this);
				return;
			}

			BeforePackTexture(texture);

			// 取期望宽高与贴图实际宽高中的较小值作为最终宽高
			var size = GetSize();
			var width = Mathf.Min(size.x, texture.width);
			var height = Mathf.Min(size.y, texture.height);

			// 子贴图已存在，但与新贴图尺寸不一致，需要销毁旧贴图
			if(textureAtlas != null && (position.width != width || position.height != height))
			{
				OnDestroy();
			}

			if(textureAtlas != null)
			{
				// 更新子贴图内容
				textureAtlas.UpdateSubTexture(this, texture, () =>
				{
					AfterPackTexture(texture);
					onSuccess?.Invoke();
				});
			}
			else
			{
				// 生成新的子贴图
				TextureAtlasManager.instance.NewSubTexture(this, texture, width, height, (atlas, pos) =>
				{
					textureAtlas = atlas;
					position = pos;

					AfterPackTexture(texture);
					onSuccess?.Invoke();
				});
			}
		}

		private void OnDestroy()
		{
			textureAtlas?.ReleaseSubTexture(this);
			textureAtlas = null;
		}
	}
}
