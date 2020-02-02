using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconGenerator : Editor
{
    private const string labelIdentifier = "Icon";
    private const string iconGeneratedSuffix = "Icon";
    
    private static readonly Vector2 textureResolution = new Vector2(256, 256);
    private static readonly Dictionary<Object, float> assetsToGenerateIconsFrom = new Dictionary<Object, float>();
    private static readonly string pathToGenerateIconsTo = "Assets/EatOrYEET/Sprites/Icons";
    private static readonly Color backgroundColor = Color.clear;
    
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
            
            // Iterate through all the labels this asset has...
            foreach (string label in assetLabels)
            {
                if (label.Contains(labelIdentifier))
                {
                    // We are assuming this is just an icon with no scalar
                    if (label.Length == labelIdentifier.Length)
                    {
                        // Cache that asset and move on to the next asset path
                        assetsToGenerateIconsFrom.Add(asset, 1); // 1 is the default scale
                        break;
                    }
                    
                    // TODO: Improve string cleaning to support multiple cases
                    assetsToGenerateIconsFrom.Add(asset, float.Parse(ExtractNumbers(label), CultureInfo
                    .InvariantCulture.NumberFormat)); // Get number on the label and save it as our scalar
                }
            }
        }

        LogFetchedAssets();
    }
    
    private static bool IsAssetLabeled(Object asset, out string[] labels)
    {
        labels = AssetDatabase.GetLabels(asset);
        return labels.Length > 0;
    }

    private static string ExtractNumbers(string stringToExtractNumbersFrom)
    {
        if (string.IsNullOrEmpty(stringToExtractNumbersFrom))
        {
            return stringToExtractNumbersFrom;
        }
        
        Regex filter = new Regex(@"[^\d]+");
        return filter.Replace(stringToExtractNumbersFrom, "");
    }

    [MenuItem("Adrian Miasik/Log Fetched Assets")]
    private static void LogFetchedAssets()
    {
        Debug.Log("Total Fetched Assets: [" + assetsToGenerateIconsFrom.Count + "].");

        foreach (KeyValuePair<Object, float> obj in assetsToGenerateIconsFrom)
        {
            Debug.Log(obj.Key.name + " scaled by " + obj.Value, obj.Key);
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
    
    [MenuItem("Adrian Miasik/Generate Icons")]
    private static void GenerateIcons()
    {
        if (assetsToGenerateIconsFrom.Count <= 0)
            return;

        Directory.CreateDirectory(pathToGenerateIconsTo);

        // Generate an icon with a specific scale
        foreach (KeyValuePair<Object, float> asset in assetsToGenerateIconsFrom)
        {
            GenerateIcon(asset);
        }
    }

    // TODO: Separation of concerns
    /// <summary>
    /// Generates an icon (using the asset and it's scalar)
    /// </summary>
    /// <param name="asset">Key is the actual asset, value is the assets transform scalar</param>
    private static void GenerateIcon(KeyValuePair<Object, float> asset)
    {
        // Create a scene and load it
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        
        // Spawn objects
        GameObject spawnedObject = Instantiate(asset.Key, Vector3.down, Quaternion.Euler(new Vector3(0,45,0))) as 
        GameObject;
        
        spawnedObject.transform.localScale = Vector3.one * asset.Value; // Scale asset based on scalar

        // Change background
        Camera cam = Camera.main;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = backgroundColor;

        // Look at object
        Renderer assetRenderer = spawnedObject.GetComponent<Renderer>();
        if (assetRenderer != null)
        {
            cam.transform.LookAt(assetRenderer.bounds.center);
        }
        else
        {
            cam.transform.LookAt(spawnedObject.transform);
        }

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
        assetSprite.name = asset.Key.name + iconGeneratedSuffix;
        
        // Create our sprite at location
        string assetPath = pathToGenerateIconsTo + "/" + assetSprite.name + ".png";
        File.WriteAllBytes(assetPath, texture.EncodeToPNG());
        
        renderTexture.Release();
    }

    [MenuItem("Adrian Miasik/Fetch and Generate Icons")]
    private static void FetchAndGenerateIcons()
    {
        FetchAssets();
        GenerateIcons();
    }
}