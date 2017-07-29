using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class VNImage : MonoBehaviour
{
    private Image image;
    public Image Image
    {
        get
        {
            if (!image)
                Start();
            return image;
        }
    }

    private Animator animator;
    public Animator Animator
    {
        get
        {
            if (!animator)
                Start();
            return animator;
        }
    }

    private VNImagePanel panel;
    public VNImagePanel Panel
    {
        get
        {
            if (!panel)
                Start();
            return panel;
        }
    }

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        panel = GetComponentInParent<VNImagePanel>();
    }

    public void OnShowComplete()
    {
        Panel.OnShowComplete();
    }

    public void OnHideComplete()
    {
        image.sprite = null;
        Panel.OnHideComplete();
    }

    public void Hide()
    {
        // Begin animation
        Animator.Play("Hide");
    }
    public void Show(Sprite sprite)
    {
        gameObject.SetActive(true);

        // Set sprite
        Image.sprite = sprite;

        // Set color to transparent
        Color temp = Image.color;
        temp.a = 0;
        Image.color = temp;

        // Begin animation
        Animator.Play("Show");
    }
}
