using System;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private float minAlpha, fadeMultiplier, delay; // Keep fadeMultiplier private

    TextMeshProUGUI textComponent;
    Color originalColor;

    bool initialized;
    string messageText;
    float alpha;
    float delayCounter;
    int intValue;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (initialized)
            return;

        initialized = true;
        textComponent = GetComponent<TextMeshProUGUI>();
        originalColor = textComponent.color;
    }

    void Update()
    {
        if (alpha == 1)
            delayCounter -= Time.deltaTime;
        if (delayCounter <= 0)
        {
            alpha -= fadeMultiplier * Time.deltaTime;
            if (alpha < minAlpha)
            {
                alpha = 1;
                delayCounter = delay;
            }
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
        textComponent.text = messageText.Replace("<ALPHA>", GetAlpha(alpha));
    }

    string GetAlpha(float value)
    {
        intValue = Mathf.CeilToInt(value * 255);
        return Convert.ToString(intValue, 16);
    }

    // Method to adjust the blink speed
    public void SetBlinkSpeed(float newFadeMultiplier)
    {
        fadeMultiplier = newFadeMultiplier; // Update fadeMultiplier using this method
    }

    public void SetMessage(bool visible, string text)
    {
        Initialize();
        gameObject.SetActive(visible);
        messageText = text;
        alpha = 1;
        delayCounter = delay;

        textComponent.text = messageText.Replace("<ALPHA>", GetAlpha(alpha));
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
