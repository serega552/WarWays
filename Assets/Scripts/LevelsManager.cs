using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class LevelsManager : MonoBehaviour
{
    private TMP_Text _allScore;
    private int _totalScore = 0;
    private bool _isFirstRecord = true;

    public int TotalScore => _totalScore;
    
    private void Awake()
    {
        _allScore = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        _allScore.text = _totalScore.ToString();
    }

    public void SumScore(int score)
    {
        _totalScore += score;

        if (_isFirstRecord)
        {
            YandexGame.NewLeaderboardScores("Leaderboard", _totalScore);
            _isFirstRecord = false;
        }
    }
}
