using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AnimSprite : EditorWindow
{
    public TextureImporter tx;
    public Texture2D texture;
    public int width ;
    public int height ;
    int tab;
    [MenuItem("Window/Alpha/SmartSprite")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow<AnimSprite>("SmartSprite");
    }

    private void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, new string[] { "ConvertSprite", "Bake", "Layers" });
        switch (tab)
        {
            case 0:
                ConvertSprite();
                break;
                
        }

        void ConvertSprite()
        {
            GUILayout.Label("This is Label");
            width = EditorGUILayout.IntField("SpriteWidth", width);
            height = EditorGUILayout.IntField("SpriteHeight", height);


            if (GUILayout.Button("Reimport"))
            {
                SetupTexture();
            }
        }
            
    }

    void SetupTexture()
    {
        foreach (object obj in Selection.objects)
        {

            texture = obj as Texture2D;
            string path = AssetDatabase.GetAssetPath(texture);
            tx = AssetImporter.GetAtPath(path) as TextureImporter;
            string[] title = path.Split('/');
            string m_title = title[title.Length-1].Split('.')[0];
            List<SpriteMetaData> newData = new List<SpriteMetaData>();

            tx.textureType = TextureImporterType.Sprite;
            tx.spriteImportMode = SpriteImportMode.Multiple;
            tx.filterMode = FilterMode.Point;
            for (int i = 0; i < texture.width; i += width)
            {
                for (int j = 0; j < texture.height; j += height)
                {
                    SpriteMetaData smd = new SpriteMetaData();
                    smd.pivot = new Vector2(.5f, .5f);
                    smd.alignment = 9;
                    smd.name = m_title+(texture.height - j) / height + ", " + i / width;
                    smd.rect = new Rect(i, j, width, height);
                    newData.Add(smd);
                }
            }
            tx.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            tx.SaveAndReimport();
        }
    }
}
