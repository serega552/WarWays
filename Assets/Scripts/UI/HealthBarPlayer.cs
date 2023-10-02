using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Heart _heartTemplate;

    private List<Heart> _hearts = new List<Heart>();

    private void OnEnable()
    {
        _player.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        if (_hearts.Count < value)
        {
            float createHealth = value - _hearts.Count;

            for (int i = 0; i < createHealth; i++)
            {
                CreateHeart();
            }
        }
        else if(_hearts.Count > value)
        {
            float deleteHealth = _hearts.Count - value;

            for (int i = 0; i < deleteHealth; i++)
            {
                if(_hearts.Count > 0)
                DestroyHeart(_hearts[_hearts.Count - 1]);
            }
        }
    }

    private void CreateHeart()
    {
        Heart newHeart = Instantiate(_heartTemplate, transform);
        _hearts.Add(newHeart.GetComponent<Heart>());
        newHeart.ToFill();
    }

    private void DestroyHeart(Heart heart)
    {
        _hearts.Remove(heart);
        heart.ToEmpty();
    }
}
