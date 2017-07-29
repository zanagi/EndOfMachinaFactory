using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextPanelType
{
    Normal, Thinking, Narration
}

[RequireComponent(typeof(Image))]
public class VNTextPanel : MonoBehaviour {

    // UI Text objects set in editor
    public Text text, nameText;

    // Panel sprites/backgrounds for specific situations
    public Sprite normalBg, thinkingBg, narrationBg;

    public bool Animating { get; private set; }

    private Image background;
    private Animator animator;
    private string targetText;

    private void Start()
    {
        background = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        if (Animating || targetText == null || targetText.Length == 0)
            return;
        
		if(text.text.Length < targetText.Length)
        {
            Debug.Log("Update text");
            text.text += targetText[text.text.Length];
        }
	}

    private void SetTargetText(TextPanelType tpType, string target, bool namePanelActive = false)
    {
        // Set background sprite
        background.sprite = (tpType == TextPanelType.Normal) ? normalBg : (tpType == TextPanelType.Thinking ? thinkingBg : narrationBg);

        // Show/Hide name panel
        nameText.transform.parent.gameObject.SetActive(namePanelActive);

        // Update target text
        text.text = string.Empty;
        targetText = target;
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
        get { return text.text == targetText; }
    }
}
