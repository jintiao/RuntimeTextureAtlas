using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeTextureAtlas
{
	/// <summary>
	/// RawImage图集代理.
	/// </summary>
	public class RawImageAgent : SubTextureAgent
	{
		protected override Vector2Int GetSize()
		{
			var image = GetComponent<RawImage>();
			if(image == null)
				return Vector2Int.zero;
			
			RectTransform rt = image.rectTransform;
			var width = (int)(rt.sizeDelta.x * rt.localScale.x);
			var height = (int)(rt.sizeDelta.y * rt.localScale.y);

			return new Vector2Int(width, height);
		}

		protected override void BeforePackTexture(Texture texture) 
		{
			var image = GetComponent<RawImage>();
			if(image == null)
				return;

			// 在图集打包完成之前，先使用原贴图.
			image.texture = texture;
			image.uvRect = new Rect(0, 0, 1, 1);
		}

		protected override void AfterPackTexture(Texture texture)
		{
			var image = GetComponent<RawImage>();
			if(image == null)
				return;

			// 改用图集贴图并设置对应子贴图uv
			image.texture = textureAtlas.texture;
			image.uvRect = new Rect((float)position.x / (float)textureAtlas.texture.width, 
			                        (float)position.y / (float)textureAtlas.texture.height,
			                        (float)position.width / (float)textureAtlas.texture.width, 
			                        (float)position.height / (float)textureAtlas.texture.height);
		}
	}
}
