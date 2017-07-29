using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ImagePanelPosition
{
    Left, Right, All
}

public class VNImagePanel : MonoBehaviour
{
    public bool Complete { get; private set; } // Is last action complete?

    public VNImage left, right;

    private VNImage target;
    private Sprite nextSprite;
    private static readonly string hideKeyword = "Hide";

    public void SetImage(string name, ImagePanelPosition position)
    {
        if (position == ImagePanelPosition.All)
        {
            SetImage(name, ImagePanelPosition.Left);
            SetImage(name, ImagePanelPosition.Right);
            return;
        }
        Complete = false;
        target = (position == ImagePanelPosition.Left) ? left : right;
        
        if (name == hideKeyword)
        {
            target.Hide();
            return;
        }
        Sprite sprite = ContentManager.Instance.GetSprite(name);

        if (sprite)
        {
            if (target.gameObject.activeSelf)
            {
                if (target.Image.sprite == sprite)
                {
                    Complete = true;
                    return;
                }
                nextSprite = sprite;
                target.Hide();
            }
            target.Show(sprite);
        }
    }
    
    public void OnShowComplete()
    {
        Complete = true;
    }

    public void OnHideComplete()
    {
        target.gameObject.SetActive(false);
        if (nextSprite)
        {
            target.Show(nextSprite);
            nextSprite = null;
            return;
        }
        Complete = true;
    }

    public void HideImage(ImagePanelPosition position)
    {
        Complete = false;
        target = (position == ImagePanelPosition.Left) ? left : right;

        if(!target.gameObject.activeSelf)
        {
            Complete = true;
            return;
        }
    }
}
