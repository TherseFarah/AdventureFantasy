using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Button backgroundMusicButton;
    [SerializeField] AudioListener audioListener;
    [SerializeField] Sprite audioOnSprite; 
    [SerializeField] Sprite audioOffSprite;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = backgroundMusicButton.GetComponent<Image>();
        backgroundMusicButton.onClick.AddListener(() => ToggleAudio());
    }
    void ToggleAudio()
    {
        audioListener.enabled = !audioListener.enabled;
        UpdateButtonImage();
    }
    void UpdateButtonImage()
    {
        if (audioListener.enabled)
        {
            buttonImage.sprite = audioOnSprite;
        }
        else
        {
            buttonImage.sprite = audioOffSprite;
        }
    }
}
