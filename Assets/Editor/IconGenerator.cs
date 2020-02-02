using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconGenerator : Editor
{
    private const string labelIdentifier = "Icon";
    private const string iconGeneratedSuffix = "Icon";
    
    private static readonly Vector2 textureResolution = new Vector2(256, 256);
    private static readonly List<Object> assetsToGenerateIconsFrom = new List<Object>();
    private static readonly string pathToGenerateIconsTo = "Assets/EatOrYEET/Sprites/Icons";
    private static readonly Color backgroundColor = Color.clear;
    
    private int spawnedObject;

    [MenuItem("Adrian Miasik/Fetch Assets")]
    private static void FetchAssets()
    {
        // Clear any assets we may have previously found
        ClearFetchedAssets();
        
        // Get all asset paths in the game
        string[] paths = AssetDatabase.GetAllAssetPaths();
        
        // Iterate through each asset path
        foreach (string path in paths)
        {
            Object asset = AssetDatabase.LoadMainAssetAtPath(path);
            
            // If asset is not labeled, lets look at other asset paths
            if (!IsAssetLabeled(asset, out string[] assetLabels))
                continue;
            
            // Iterate through all the labels the asset has...
            foreach (string label in assetLabels)
            {
                // If that label matches our identifier...
                if (label == labelIdentifier)
                {
                    // Cache that asset and move on to the next asset path
                    assetsToGenerateIconsFrom.Add(asset);
                    break;
                }
            }
        }

        LogFetchedAssets();
    }

    [MenuItem("Adrian Miasik/Log Fetched Assets")]
    private static void LogFetchedAssets()
    {
        Debug.Log("Total Fetched Assets: [" + assetsToGenerateIconsFrom.Count + "].");

        foreach (Object obj in assetsToGenerateIconsFrom)
        {
            Debug.Log(obj.name, obj);
        }
    }
    
    private static void ClearFetchedAssets()
    {
        if (assetsToGenerateIconsFrom.Count <= 0)
        {
            return;
        }
        assetsToGenerateIconsFrom.Clear();
    }

    private static bool IsAssetLabeled(Object asset, out string[] labels)
    {
        labels = AssetDatabase.GetLabels(asset);
        return labels.Length > 0;
    }
    
    [MenuItem("Adrian Miasik/Generate Icons")]
    private static void GenerateIcons()
    {
        if (assetsToGenerateIconsFrom.Count <= 0)
            return;

        Directory.CreateDirectory(pathToGenerateIconsTo);
        
        for (int i = 0; i < assetsToGenerateIconsFrom.Count; i++)
        {
            GenerateIcon(assetsToGenerateIconsFrom[i]);
        }
    }

    // TODO: Separation of concerns
    private static void GenerateIcon(Object asset)
    {
        // Create a scene and load it
        Scene iconScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        
        // Spawn object
        GameObject spawnedObject = Instantiate(asset, Vector3.zero, Quaternion.Euler(new Vector3(0, 90, -35))) as GameObject;
        spawnedObject.transform.localScale = Vector3.one * 3;

        // Change background
        Camera cam = Camera.main;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = backgroundColor;
        
        // Define size
        int iconWidth = (int)textureResolution.x;
        int iconHeight = (int)textureResolution.y;
        
        // Create a render texture and render camera to it
        RenderTexture renderTexture = new RenderTexture(iconWidth, iconHeight, 32, RenderTextureFormat.ARGB32);
        renderTexture.name = "Auto Generated Texture";
        cam.targetTexture = renderTexture;
        cam.Render();
        
        // Create a texture
        Texture2D texture = new Texture2D(iconWidth, iconHeight, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0,0,iconWidth, iconHeight),0,0, false);
        texture.Apply();
        
        // Create a sprite
        Sprite assetSprite = Sprite.Create(texture, Rect.zero, new Vector2(0.5f, 0.5f));
        assetSprite.name = asset.name + iconGeneratedSuffix;
        
        // Create our sprite at location
        string assetPath = pathToGenerateIconsTo + "/" + assetSprite.name + ".png";
        File.WriteAllBytes(assetPath, texture.EncodeToPNG());
    }

    [MenuItem("Adrian Miasik/Fetch and Generate Icons")]
    private static void FetchAndGenerateIcons()
    {
        FetchAssets();
        GenerateIcons();
    }
}