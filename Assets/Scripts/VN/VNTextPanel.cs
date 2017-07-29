using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextPanelType
{
    Normal, Thinking, Narration
}

[RequireComponent(typeof(Image))]
public class VNTextPanel : MonoBehaviour {
    
    [SerializeField]
    private Text text;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Sprite normalBg;
    [SerializeField]
    private Sprite thinkingBg;
    [SerializeField]
    private Sprite narrationBg;

    public bool Animating { get; private set; }

    private Image background;
    private Animator animator;
    private string targetText;
    private int textIndex;

    private StringBuilder sb;
    private string colorTagPrefix = "<color=#00000000>", colorTagSuffix = "</color>";

    private void Start()
    {
        background = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        if (Animating || targetText == null || targetText.Length == 0)
            return;
        
		if(textIndex < targetText.Length)
        {
            sb = new StringBuilder();
            sb.Append(targetText.Substring(0, textIndex + 1));
            sb.Append(colorTagPrefix);
            sb.Append(targetText.Substring(textIndex + 1));
            sb.Append(colorTagSuffix);
            text.text = sb.ToString();
            textIndex++;
        }
	}

    private void SetTargetText(TextPanelType tpType, string target, bool namePanelActive = false)
    {
        // Set background sprite
        background.sprite = (tpType == TextPanelType.Normal) ? normalBg : (tpType == TextPanelType.Thinking ? thinkingBg : narrationBg);

        // Show/Hide name panel
        nameText.transform.parent.gameObject.SetActive(namePanelActive);

        // Update target text
        text.text = new string(' ', target.Length);
        targetText = target;
        textIndex = 0;
    }

    public void SetTargetTextAndName(TextPanelType tpType, string target, string characterName)
    {
        if (!animator)
            Start();
        
        // Check animation
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            animator.SetBool("Show", true);
            Animating = true;
        }

        if (characterName == null || characterName.Length == 0)
        {
            SetTargetText(tpType, target, false);
            return;
        }
        SetTargetText(tpType, target, true);
        nameText.text = characterName;
    }

    public void OnShowComplete()
    {
        Animating = false;
    }

    public void OnHideComplete()
    {
        Animating = false;
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        text.text = string.Empty;
        Animating = true;
        animator.SetBool("Hide", true);
    }

    public void CompleteText()
    {
        text.text = targetText;
    }

    public bool Complete
    {
        get { return textIndex == targetText.Length; }
    }
}
