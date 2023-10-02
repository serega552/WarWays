using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private WeaponCard[] _autoWeaponCards;
    [SerializeField] private WeaponCard[] _sniperWeaponCards;
    [SerializeField] private Image _equipWeapon1;
    [SerializeField] private Image _equipWeapon2;

    private int _money;
    private string _auto = "auto";
    private string _sniper = "sniper";
    private string _typeWeapon;
    private WeaponCard _currentWeaponCard;

    public event UnityAction<int> OnChangeMoney;

    private void Awake()
    {
        _money = PlayerPrefs.GetInt("Money");
    }

    public void SelectWeapon(WeaponCard weaponCard)
    {
        _currentWeaponCard = weaponCard;
        if (_currentWeaponCard.GetComponent<AutoWeapon>() != null)
            _typeWeapon = _auto;
        else if (_currentWeaponCard.GetComponent<SniperWeapon>() != null)
            _typeWeapon = _sniper;

        if (_typeWeapon == _auto)
        {
            if (_money >= _currentWeaponCard.PriceWeapon && _currentWeaponCard.IsBought == false)
            {
                _equipWeapon1.sprite = _currentWeaponCard.CurrentWeapon;
                BuyWeapon();
            }
            else if (_currentWeaponCard.IsBought && _currentWeaponCard.IsEquipped == false)
            {
                TurnOffWeapons();
                _equipWeapon1.sprite = _currentWeaponCard.CurrentWeapon;
                _currentWeaponCard.Equip(_typeWeapon, _auto, _sniper);
            }
            else if (_currentWeaponCard.IsBought == false)
            {

            }
        }
        else if (_typeWeapon == _sniper)
        {
            if (_money >= _currentWeaponCard.PriceWeapon && _currentWeaponCard.IsBought == false)
            {
                _equipWeapon2.sprite = _currentWeaponCard.CurrentWeapon;
                BuyWeapon();

            }
            else if (_currentWeaponCard.IsBought && _currentWeaponCard.IsEquipped == false)
            {
                TurnOffWeapons();
                _equipWeapon2.sprite = _currentWeaponCard.CurrentWeapon;
                _currentWeaponCard.Equip(_typeWeapon, _auto, _sniper);
            }
            else if (_currentWeaponCard.IsBought == false)
            {

            }
        }
    }

    public void SetPicture(WeaponCard weaponCard)
    {
        _currentWeaponCard = weaponCard;

        if (_currentWeaponCard.GetComponent<AutoWeapon>() != null)
            _equipWeapon1.sprite = _currentWeaponCard.CurrentWeapon;
        else if (_currentWeaponCard.GetComponent<SniperWeapon>() != null)
            _equipWeapon2.sprite = _currentWeaponCard.CurrentWeapon; ;
    }

    private void BuyWeapon()
    {
        _money -= _currentWeaponCard.PriceWeapon;

        TurnOffWeapons();

        _currentWeaponCard.Buy();
        _currentWeaponCard.Equip(_typeWeapon, _auto, _sniper);

        OnChangeMoney?.Invoke(_money);

        PlayerPrefs.SetInt("Money", _money);
    }

    private void TurnOffWeapons()
    {
        if (_typeWeapon == _sniper)
        {
            foreach (var weapon in _sniperWeaponCards)
            {
                weapon.UnEquip(_typeWeapon, _auto, _sniper);
            }
        }
        if (_typeWeapon == _auto)
        {
            foreach (var weapon in _autoWeaponCards)
            {
                weapon.UnEquip(_typeWeapon, _auto, _sniper);
            }
        }
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
