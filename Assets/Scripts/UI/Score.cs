using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using YG.Utils.LB;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Player _player;

    private int _sumScore;

    public int SumScore => _sumScore;

    private void OnEnable()
    {
        _player.OnScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _player.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _sumScore += score;
        _score.text = _sumScore.ToString();
    }
}