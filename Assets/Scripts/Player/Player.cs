using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip[] _painClips;
    [SerializeField] private PainScreen _painScreen;
    [SerializeField] private Weapon[] _autoWeapon;
    [SerializeField] private Weapon[] _sniperWeapon;
    [SerializeField] protected Image _aim;
    [SerializeField] private DieScreen _dieScreen;

    private Weapon _weapon1;
    private Weapon _weapon2;
    private string _weaponName1;
    private string _weaponName2;
    private AudioSource _audioSourse;
    private float _health = 3;
    private float _currentHealth;
    private int _countPain = 0;
    private int _hitScore;
    private int _money = 0;
    private bool _isDied = false;

    public event UnityAction<float> OnHealthChanged;
    public event UnityAction<int> OnScoreChanged;
    public bool IsDied => _isDied;
    public int Money => _money;
    public int HitScore => _hitScore;
    public Image Aim => _aim;

    private void Start()
    {
        OnHealthChanged?.Invoke(_health);
    }

    private void Awake()
    {
        _money = PlayerPrefs.GetInt("Money");

        _audioSourse = GetComponent<AudioSource>();

        _currentHealth = _health;

        _weaponName1 = PlayerPrefs.GetString("WeaponName1");
        _weaponName2 = PlayerPrefs.GetString("WeaponName2");

        SelectGuns();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeepOneWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeepTwoWeapon();
        }
    }

    public void SelectGuns()
    {
        if (_weaponName1 == null)
        {
            _weaponName1 = "Ppsh";
        }

        if (_weaponName2 == null)
        {
            _weaponName2 = "Mosin";
        }

        foreach (Weapon weapon in _autoWeapon)
        {
            if (weapon.NameGun == _weaponName1)
            {
                _weapon1 = weapon;
            }
        }

        foreach (Weapon weapon in _sniperWeapon)
        {
            if (weapon.NameGun == _weaponName2)
            {
                _weapon2 = weapon;
            }
        }

        KeepOneWeapon();
    }

    public void TakeDamage(float damage)
    {
        _audioSourse.PlayOneShot(_painClips[_countPain]);
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);
        _countPain++;

        _painScreen.Open();

        if (_currentHealth <= 0)
            Die();

        if (_countPain >= _painClips.Length)
            _countPain = 0;
    }

    public void KeepOneWeapon()
    {
        if (_weapon1 != null && _weapon2 != null)
        {
            _weapon1.gameObject.SetActive(true);
            _weapon2.gameObject.SetActive(false);
        }
    }

    public void KeepTwoWeapon()
    {
        if (_weapon1 != null && _weapon2 != null)
        {
            _weapon2.gameObject.SetActive(true);
            _weapon1.gameObject.SetActive(false);
        }
    }

    public void AddScore(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    private void Die()
    {
        _isDied = true;
        _dieScreen.Open();
    }

    public void Resurrect()
    {
            _isDied = false;
            _currentHealth = _health;
            OnHealthChanged?.Invoke(_currentHealth);
            GetComponent<PlayerController>().DeleteEnemy();
            _dieScreen.Close();
    }

    public void GetMoney(int money)
    {
        _money += money;
    }
}
