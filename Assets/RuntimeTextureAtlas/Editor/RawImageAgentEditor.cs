using UnityEngine;
using UnityEditor;

namespace RuntimeTextureAtlas
{
	[CustomEditor(typeof(RawImageAgent))]
	public class RawImageAgentEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var agent = ((RawImageAgent)target);

			if(agent.textureAtlas == null)
				return;

			EditorGUILayout.LabelField("SubTexture Position");
			EditorGUILayout.LabelField(string.Format("{0}, {1}, {2}, {3}", agent.position.x, agent.position.y, agent.position.width, agent.position.height));

			GUILayout.Space(10);
			var textureAtlas = agent.textureAtlas;
			var width = textureAtlas.texture.width;
			var height = textureAtlas.texture.height;
			EditorGUILayout.LabelField(string.Format("Atlas Size: {0} X {1}", width, height));
			EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetAspectRect((float)width / height), textureAtlas.texture);
		}
	}
}
