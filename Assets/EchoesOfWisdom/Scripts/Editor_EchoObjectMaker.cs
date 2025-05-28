using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEditor;
using UnityEngine;

public class Editor_EchoObjectMaker : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform listTransform;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    [ButtonMethod]
    private void FindSprites()
    {
        sprites.Clear();
        
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] {"Assets/EchoesOfWisdom/Sprites/Echoes"});

        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite != null)
            {
                sprites.Add(sprite);
            }
        }
    }
    
    [ButtonMethod]
    private void GenerateEchoObjects()
    {
        foreach (var sprite in sprites)
        {
            GameObject echoObject = Instantiate(prefab, listTransform);
            Echo newEchoComponent = echoObject.GetComponent<Echo>();
            newEchoComponent.Initialize(sprite);
        }
    }
}
