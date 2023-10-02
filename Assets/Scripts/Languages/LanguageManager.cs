using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LanguageManager : MonoBehaviour
{
    public int Language;

    private YandexGame _yandexGame;

    private void GetData()
    {
        string language = YandexGame.EnvironmentData.language;

        if (PlayerPrefs.GetInt("languageSelected") == 0 && YandexGame.SDKEnabled)
        {
            if (language == "ru" || language == "be" || language == "kk" || language == "uk" || language == "uz")
                RussianLanguage();
            else if (language == "tr")
                TurkishLanguage();
            else
                EnglishLanguage();
        }
    }

    private void Awake()
    {
        _yandexGame = GetComponent<YandexGame>();

        if (PlayerPrefs.GetInt("languageSelected") != 1)
        {
            GetData();
            PlayerPrefs.SetInt("languageSelected", 1);
        }
        else
        {
            Language = PlayerPrefs.GetInt("language", Language);
        }
    }

    public void RussianLanguage()
    {
        Language = 0;
        PlayerPrefs.SetInt("language", Language);
        MenuScene.Load();
    }

    public void EnglishLanguage()
    {
        Language = 1;
        PlayerPrefs.SetInt("language", Language);
        MenuScene.Load();
    }

    public void TurkishLanguage()
    {
        Language = 2;
        PlayerPrefs.SetInt("language", Language);
        MenuScene.Load();
    }
}
