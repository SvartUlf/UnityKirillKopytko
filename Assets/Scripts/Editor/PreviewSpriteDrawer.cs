using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Game.BuildSystem
{
    [CustomEditor(typeof(BuildTileData))]
    public class PreviewSpriteDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BuildTileData tileData = (BuildTileData)target;

            if (tileData.TileSprite)
            {
                GUILayout.Space(15);

                Texture2D texture = tileData.TileSprite.texture;

                Rect rect = tileData.TileSprite.rect;
                Rect uv = new Rect(rect.x / texture.width, rect.y / texture.height, rect.width / rect.height, rect.height / texture.height);

                uv.width = rect.width / texture.width;
                uv.height = rect.height / texture.height;

                Rect previewRect = GUILayoutUtility.GetRect(128, 128,
                    GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

                previewRect.x = (EditorGUIUtility.currentViewWidth - 128) / 2;

                GUI.DrawTextureWithTexCoords(previewRect, texture, uv);
            }
        }
    }
}