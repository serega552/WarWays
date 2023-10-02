using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames.Samples;
using Agava.YandexGames;
using YG;

public class RewardAdsManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private YandexGame _yandexGame;

    public void AdButton()
    {
        _yandexGame._RewardedShow(1);
    }

    public void AdButtonCul()
    {
        _player.Resurrect();
    }
}
