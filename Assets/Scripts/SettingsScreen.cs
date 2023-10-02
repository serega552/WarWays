using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : Screenn
{
    [SerializeField] private Slider _soundSettings;
    [SerializeField] private Slider _sensitivitySettings;

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            PlayerPrefs.SetInt("platform", 1);
        }
        else
        {
            PlayerPrefs.SetInt("platform", 0);
        }

        if (PlayerPrefs.GetInt("platform") == 1)
        {
            PlayerPrefs.SetFloat("MobileSensitivitySettings", _sensitivitySettings.value = 0.2f);
            _sensitivitySettings.minValue = 0.01f;
            _sensitivitySettings.maxValue = 0.5f;
        }
        else
        {
            PlayerPrefs.SetFloat("DecktopSensitivitySettings", _sensitivitySettings.value = 0.9f);
            _sensitivitySettings.minValue = 0.1f;
            _sensitivitySettings.maxValue = 5f;
        }


        if (PlayerPrefs.GetInt("FirstPlay") != 1)
        {            
            PlayerPrefs.SetFloat("SoundSettings", _soundSettings.value = 1);


            PlayerPrefs.SetInt("FirstPlay", 1);
        }

        if (PlayerPrefs.GetInt("platform") == 1)
            _sensitivitySettings.value = PlayerPrefs.GetFloat("MobileSensitivitySettings");
        else
            _sensitivitySettings.value = PlayerPrefs.GetFloat("DecktopSensitivitySettings");

        _soundSettings.value = PlayerPrefs.GetFloat("SoundSettings");

        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }

    public override void Open()
    {
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;

        AudioManager.instance.Play("Click");
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;

        PlayerPrefs.SetFloat("SoundSettings", _soundSettings.value);

        if (PlayerPrefs.GetInt("platform") == 1)
            PlayerPrefs.SetFloat("MobileSensitivitySettings", _sensitivitySettings.value);
        else
            PlayerPrefs.SetFloat("DecktopSensitivitySettings", _sensitivitySettings.value);

        AudioListener.volume = _soundSettings.value;
    }
}
