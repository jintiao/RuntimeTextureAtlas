using UnityEngine;
using UnityEditor;

namespace RuntimeTextureAtlas
{
	[CustomEditor(typeof(TextureAtlasManager))]
	public class TextureAtlasManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if(EditorApplication.isPlaying)
			{
				DrawPlayModeGUI();
			}
			else
			{
				DrawDefaultInspector();
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void DrawPlayModeGUI()
		{
			var textureAtlas = ((TextureAtlasManager)target).textureAtlas;
			var width = textureAtlas.texture.width;
			var height = textureAtlas.texture.height;

			GUILayout.Space(10);
			EditorGUILayout.LabelField(string.Format("Atlas Size: {0} X {1}", width, height));
			EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetAspectRect((float)width / height), textureAtlas.texture);
		}
	}
}
