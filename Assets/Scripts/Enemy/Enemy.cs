using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Point _point;

    private ParticleSystem _dieEffect;
    private float _health = 100f;    
    private float _currentHealth;
    private float _damage = 1f;
    private int _price = 5;
    private Player _player;
    private EnemyController _enemyController;
    private Animator _animator;
    private bool _isDied = false;
    private int _headScore = 100;
    private int _bodyScore = 50;

    public bool IsDied => _isDied;
    public float Damage => _damage;
    public Player Player => _player;
    public event UnityAction<float,float> HealthChanged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyController = GetComponent<EnemyController>();
        _dieEffect = GetComponentInChildren<ParticleSystem>();
        _player = _point.PlayerController.GetComponent<Player>();
        _currentHealth = _health;

        if (_point == null)
            gameObject.SetActive(false);
    }

    public void TakeDamage(float damage, string hitPoint)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        AudioManager.instance.Play("HitEnemy");

        if (_currentHealth <= 0)
        {
            if (hitPoint == "body")
                _player.AddScore(_bodyScore);
            else if (hitPoint == "head")
                _player.AddScore(_headScore);

                Die();
        }
    }

    public void Die()
    {
        if (_isDied == false)
        {
            _point.DieEnemy();

            if (gameObject.TryGetComponent<Sniper>(out Sniper sniper))
            {
                sniper.TurnOffImage();
                sniper.Stop();
            }

            _enemyController.Stop();
            _animator.SetTrigger("Die");

            if (_dieEffect != null)
                _dieEffect.Play();

            _isDied = true;

            _player.GetMoney(_price);
            
            Invoke("Destroing", 4f);
        }
    }

    private void Destroing()
    {
        gameObject.SetActive(false);
    }
}
