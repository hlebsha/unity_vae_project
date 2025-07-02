using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SteamAudio;

public class AudioPluginSwitcher : MonoBehaviour
{
    public AudioSource[] audioSources; 
    public GameObject listenerObject;  
    public TextMeshProUGUI audioStatusText; 

    private bool isUsingSteamAudio = true;

    void Start()
    {
        ConfigureListeners();
        SetupAudioComponents();
        UpdateUIText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchAudioPlugins();
            UpdateUIText();
        }
    }

    void ConfigureListeners()
    {
        var steamListener = listenerObject.GetComponent<SteamAudioListener>();
        var standardListener = listenerObject.GetComponent<AudioListener>();
        
        if (isUsingSteamAudio)
        {
            if (steamListener) steamListener.enabled = true;
            if (standardListener) standardListener.enabled = true; // Keep enabled for baseline functionality
        }
        else
        {
            if (steamListener) steamListener.enabled = false;
            if (standardListener) standardListener.enabled = true; // Ensure default listener remains active
        }
    }

    void SetupAudioComponents()
    {
        foreach (var audioSource in audioSources)
        {
            var steamSource = audioSource.GetComponent<SteamAudioSource>();
            if (steamSource)
            {
                steamSource.enabled = isUsingSteamAudio;
            }
        }
    }

    void SwitchAudioPlugins()
    {
        isUsingSteamAudio = !isUsingSteamAudio;
        ConfigureListeners();
        SetupAudioComponents();
        
        Debug.Log("Switched to " + (isUsingSteamAudio ? "Steam Audio" : "Meta XR Audio"));
    }

    void UpdateUIText()
    {
        if (audioStatusText != null)
        {
            audioStatusText.text = "Using " + (isUsingSteamAudio ? "Steam Audio" : "Meta XR Audio");
        }
    }
}