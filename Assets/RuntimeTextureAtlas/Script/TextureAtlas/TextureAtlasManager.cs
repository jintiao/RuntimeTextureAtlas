using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// 图集管理器.
	/// 用于维护图集对象.
	/// </summary>
	public class TextureAtlasManager : MonoBehaviour
	{
		/// <summary>
		/// 默认图集贴图尺寸
		/// </summary>
		[SerializeField]
		private int atlasSize = 1024;

		/// <summary>
		/// 默认图集贴图格式
		/// </summary>
		[SerializeField]
		private RenderTextureFormat textureFormat = RenderTextureFormat.Default;

		// 图集对象，目前只支持一个，如需要可以改成多个
		private TextureAtlas _textureAtlas;

		/// <summary>
		/// 获取图集对象
		/// </summary>
		public TextureAtlas textureAtlas
		{
			get
			{
				if (_textureAtlas == null)
				{
					_textureAtlas = new TextureAtlas(atlasSize, textureFormat);
				}

				return _textureAtlas;
			}
		}

		/// <summary>
		/// 图集管理器单例对象
		/// </summary>
		private static TextureAtlasManager _instance;

		/// <summary>
		/// 获取图集管理器单例对象
		/// </summary>
		public static TextureAtlasManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<TextureAtlasManager>();
					if(_instance == null)
					{
						var name = "TextureAtlasManager";
						var go = GameObject.Find(name);
						if (go == null)
							go = new GameObject(name);

						_instance = go.AddComponent<TextureAtlasManager>();
					}
				}
				return _instance;
			}
		}

		private void Awake()
		{
			// 只允许有一个对象
			if(_instance != null && _instance != this)
			{
				Debug.LogWarning("TextureAtlasManager已存在，请勿重复创建!");
				Destroy(this);
				return;
			}

			_instance = this;
		}

		private void OnDestroy()
		{
			_textureAtlas?.Destroy();
			_textureAtlas = null;

			if(_instance == this)
				_instance = null;
		}

		/// <summary>
		/// 为图集生成新的子贴图.
		/// </summary>
		public void NewSubTexture(SubTextureAgent agent, Texture texture, int width, int height, Action<TextureAtlas, RectInt> onSuccess)
		{
			textureAtlas.NewSubTexture(agent, texture, width, height, onSuccess);
		}
	}
}
