using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private PauseScreen _pause;
    [SerializeField] private Score _score;
    [SerializeField] private int _oneStarScore;
    [SerializeField] private int _twoStarScore;
    [SerializeField] private Image[] _stars;

    private int _countStar;
    private int _sumScore;
    private PlayerStatistics _localPlayerData;

    private void Start()
    {
        _localPlayerData = GlobalControl.Instance.SavedPlayerData;
        _pause.Open();
    }

    private void Awake()
    {
        _countStar = PlayerPrefs.GetInt("stars" + SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        _winScreen.Open();
        
        _sumScore = _score.SumScore;
        _localPlayerData.Score = _sumScore;
        SaveData();

        PlayerPrefs.SetInt("Money", _player.Money);

        AddStars();
    }

     private void AddStars()
    {
        if (_oneStarScore != 0 && _twoStarScore != 0)
        {
            if (_sumScore <= _oneStarScore && _countStar <= 1)
            {
                PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().name, 1);
                _stars[0].color = Color.white;
            }
            else if (_sumScore > _oneStarScore && _sumScore <= _twoStarScore && _countStar <= 2)
            {
                PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().name, 2);
                _stars[0].color = Color.white;
                _stars[1].color = Color.white;
            }
            else if (_sumScore > _twoStarScore)
            {
                PlayerPrefs.SetInt("stars" + SceneManager.GetActiveScene().name, 3);
                _stars[0].color = Color.white;
                _stars[1].color = Color.white;
                _stars[2].color = Color.white;
            }
        }
        else
        {
            Debug.Log("Не установлены значения Score в GameUI");
        }
    }

    private void SaveData()
    {
        GlobalControl.Instance.SavedPlayerData = _localPlayerData;
    }
}
