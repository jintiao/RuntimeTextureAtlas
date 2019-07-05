using RuntimeTextureAtlas;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class main : MonoBehaviour
{
	// ui界面控件
	public RawImage flag1;
	public RawImage flag2;
	public RawImage fruit1;
	public RawImage fruit2;
	public RawImage fruit3;

	// 图片资源路径
	private string[] flags;
	private string[] fruits;

	private void Start()
	{
		InitUI();
		ReloadUI();
	}

	private void OnGUI()
	{
		if(GUI.Button(new Rect(10, 10, 200, 60), "Reload"))
		{
			ReloadUI();
		}
	}

	/// <summary>
	/// 初始化UI资源路径
	/// </summary>
	void InitUI()
	{
		flags = new string[] { "celestial", "flower", "greendragon", "treeolife" };
		for(int i = 0; i < flags.Length; i++)
			flags[i] = Path.Combine("Flag", flags[i]);

		fruits = new string[8];
		for(int i = 0; i < 8; i++)
			fruits[i] = @"file://" + Path.Combine(Application.dataPath, "Example", "StreamingAssets", "Fruit", string.Format("Fruit_{0}.png", i + 1));
	}

	/// <summary>
	/// 重新加载UI
	/// </summary>
	private void ReloadUI()
	{
		// 随机加载两个flags图片并显示
		Shuffle(flags);
		LoadFromResources(flag1, flags[0]);
		LoadFromResources(flag2, flags[1]);

		// 随机加载三个fruits图片并显示
		Shuffle(fruits);
		StartCoroutine(LoadFromFile(fruit1, fruits[0]));
		StartCoroutine(LoadFromFile(fruit2, fruits[1]));
		StartCoroutine(LoadFromFile(fruit3, fruits[2]));
	}

	/// <summary>
	/// 随机打乱数组元素顺序
	/// </summary>
	private void Shuffle(string[] arr)
	{
		for(var i = 0; i < arr.Length - 1; i++)
		{
			var j = Random.Range(i, arr.Length);
			var temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
		}
	}

	/// <summary>
	/// 从Resources加载贴图
	/// </summary>
	private void LoadFromResources(RawImage image, string file)
	{
		// 同步加载贴图
		var texture = Resources.Load<Texture>(file);

		// 将贴图打包到图集，并应用到RawImage控件
		image.PackTexture(texture);
	}

	/// <summary>
	/// 从外部文件加载贴图
	/// </summary>
	private IEnumerator LoadFromFile(RawImage image, string file)
	{
		// 异步加载贴图
		using(UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(file))
		{
            yield return uwr.SendWebRequest();

			if(!uwr.isNetworkError && !uwr.isHttpError)
			{
				// 贴图加载完毕
				var texture = DownloadHandlerTexture.GetContent(uwr);

				// 将贴图打包到图集，并应用到RawImage控件
				image.PackTexture(texture, 
				                  () => Destroy(texture)); // 打包成功后卸载原图
			}
		}
	}
}
