using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadSDK : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Update()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    public void GetData()
    {
        MenuScene.Load();
    }

    public void LoadScene()
    {
        MenuScene.Load();            
    }
}
