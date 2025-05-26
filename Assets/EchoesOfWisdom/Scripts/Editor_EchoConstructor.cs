using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Editor_EchoConstructor : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Initialize(Sprite sprite)
    {
        image.sprite = sprite;
        string name = sprite.name.Replace("_", " ");
        gameObject.name = name;
    }
}
