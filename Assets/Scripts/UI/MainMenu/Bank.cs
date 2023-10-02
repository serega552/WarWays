using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    [SerializeField] private WeaponSelection _shop;
    private TMP_Text _money;

    private void Start()
    {
        _money = GetComponent<TMP_Text>();
        _money.text = PlayerPrefs.GetInt("Money").ToString();
    }

    private void OnEnable()
    {
        _shop.OnChangeMoney += ChangeMoney;
    }

    private void OnDisable()
    {
        _shop.OnChangeMoney -= ChangeMoney;
    }

    public void ChangeMoney(int money) 
    {
        _money.text = money.ToString();
    }
}
