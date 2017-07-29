using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Contains references to all necessary prefabs and resources
public class ContentManager : MonoBehaviour
{
    // Create a static instance so that it can be called from anywhere in the code
    public static ContentManager Instance { get; private set; }
    
    // Array of sprites used in the game, set in the editor
    public Sprite[] sprites;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Sprite GetSprite(string name)
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == name)
                return sprites[i];
        }
        return null;
    }
}
