using System;
using UnityEngine;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// 图集.
	/// </summary>
	public class TextureAtlas
	{
		/// <summary>
		/// 打包器
		/// </summary>
		private IBinaryPacker packer;

		/// <summary>
		/// 图集贴图.
		/// </summary>
		public RenderTexture texture { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		public TextureAtlas(int size, RenderTextureFormat format)
		{
			// 贴图尺寸限制在2048内
			size = Mathf.Min(size, 2048);

			// 贴图格式校验
			if(!SystemInfo.SupportsRenderTextureFormat(format))
				format = RenderTextureFormat.Default;

			// 目前图集不支持mipmap，也不支持除Point外的采样模式
			texture  = new RenderTexture(size, size, 0, format);
			texture.filterMode = FilterMode.Point;
			texture.useMipMap = false;

			// 贪婪打包器
			packer = new GreedyPacker(size, size);
		}

		/// <summary>
		/// 销毁贴图数据
		/// </summary>
		public void Destroy()
		{
			UnityEngine.Object.Destroy(texture);
		}

		/// <summary>
		/// 生成新的子贴图.
		/// </summary>
		public void NewSubTexture(SubTextureAgent agent, Texture subtexture, int width, int height, Action<TextureAtlas, RectInt> onSuccess)
		{
			if(agent == null || subtexture == null)
			{
				Debug.LogWarning("NewSubTexture参数错误!");
				return;
			}

			// 分配空间
			var rect = packer.Insert(width, height);
			if(rect.x < 0)
				return;

			// 将子贴图内容复制到图集贴图
			TextureUtil.DrawTexture(subtexture, texture, rect);

			// 回调
			onSuccess?.Invoke(this, rect);
		}

		/// <summary>
		/// 更新子贴图内容.
		/// </summary>
		public void UpdateSubTexture(SubTextureAgent agent, Texture subTexture, Action onSuccess)
		{
			if(agent == null || agent.textureAtlas != this)
			{
				Debug.LogWarning("UpdateSubTexture参数错误!");
				return;
			}

			TextureUtil.DrawTexture(subTexture, texture, agent.position);
			onSuccess?.Invoke();
		}

		/// <summary>
		/// 销毁子贴图
		/// </summary>
		public void ReleaseSubTexture(SubTextureAgent agent)
		{
			packer.Remove(agent.position);
		}
	}
}
