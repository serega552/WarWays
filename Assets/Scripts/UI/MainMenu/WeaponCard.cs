using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _priceWeapon;
    [SerializeField] private Sprite _currentWeapon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _checkMark;
    [SerializeField] private bool _isFirstWeapon;

    private bool _isBought = false;
    private bool _isEquipped = false;
    private WeaponSelection _weaponSelection;

    public int PriceWeapon => _priceWeapon;
    public bool IsBought => _isBought;
    public bool IsEquipped => _isEquipped;
    public Sprite CurrentWeapon => _currentWeapon;
    public string Name => _name;

    private void Awake()
    {
        _price.text = _priceWeapon.ToString();

        _weaponSelection = GetComponentInParent<WeaponSelection>();

        CheckData();

        if(_isEquipped == true)
        {
            _weaponSelection.SetPicture(this);
        }
    }

    public void OnButtonClick()
    {
        _weaponSelection.SelectWeapon(this);
    }

    public void Buy()
    {
        _isBought = true;
        _priceWeapon = 0;

        _price.text = " ";
        _checkMark.gameObject.SetActive(true);

        PlayerPrefs.SetInt(Name + "Bought", 1);

        AudioManager.instance.Play("EquippedWeapon");

        AudioManager.instance.Play("BuyWeapon");
    }

    public void Equip(string typeWeapon, string auto, string sniper)
    {
        _isEquipped = true;

        if (typeWeapon == auto)
            PlayerPrefs.SetString("WeaponName1", Name);
        else if (typeWeapon == sniper)
            PlayerPrefs.SetString("WeaponName2", Name);

        AudioManager.instance.Play("EquippedWeapon");
    }

    public void UnEquip(string typeWeapon, string auto, string sniper)
    {
        _isEquipped = false;

        if (typeWeapon == auto)
            PlayerPrefs.SetString("WeaponName1", Name);
        else if (typeWeapon == sniper)
            PlayerPrefs.SetString("WeaponName2", Name);
    }

    private void CheckData()
    {
        if (_isFirstWeapon == true && PlayerPrefs.GetInt(Name + "FirstWeapon") != 1)
        {
            _weaponSelection.SelectWeapon(this);
            PlayerPrefs.SetInt(Name + "FirstWeapon", 1);
        }

        if (PlayerPrefs.GetInt(Name + "Bought") == 1)
        {
            _isBought = true;
            _priceWeapon = 0;
            _price.text = " ";
            _checkMark.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetString("WeaponName1") == _name)
        {
            _isEquipped = true;
        }
        else if (PlayerPrefs.GetString("WeaponName2") == _name)
        {
            _isEquipped = true;
        }
    }
}

