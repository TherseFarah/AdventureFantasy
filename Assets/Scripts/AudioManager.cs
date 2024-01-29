using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Button backgroundMusicButton;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Sprite audioOnSprite; 
    [SerializeField] Sprite audioOffSprite;

    private Image buttonImage;

    private void Start()
    {
        Debug.Log("Audio Start");
        buttonImage = backgroundMusicButton.GetComponent<Image>();
        backgroundMusicButton.onClick.AddListener(() => ToggleAudio());
    }
    void ToggleAudio()
    {
        Debug.Log("Audio is being Toggled");
        audioSource.mute = !audioSource.mute;
        UpdateButtonImage();
    }
    void UpdateButtonImage()
    {
        if (!audioSource.mute)
        {
            buttonImage.sprite = audioOnSprite;
        }
        else
        {
            buttonImage.sprite = audioOffSprite;
        }
    }
}
