using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected ParticleSystem ShootEffect;
    [SerializeField] protected Camera MainCamera;
    [SerializeField] private ShootButton _shootButton;
    [SerializeField] protected Game Game;
    [SerializeField] protected TMP_Text MaxBullets;
    [SerializeField] protected TMP_Text CurrentBullets;
    [SerializeField] protected int MaxBulletCount;
    [SerializeField] private string _name;

    protected Coroutine ShootCoroutine;
    protected Coroutine ReloadCoroutine;
    protected float RealodingDelay;
    protected float ShootingDelay;
    protected int CountBulletSpent;
    protected float Range = 13f;
    protected Animator Animator;
    protected bool IsReloading = false;
    protected bool IsShooting = false;
    protected WaitForSeconds DelayReload = new WaitForSeconds(1.41f);

    public string NameGun => _name;

    public ShootButton ShootButton => _shootButton;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        CountBulletSpent = MaxBulletCount;
    }

    public abstract void Shoot();

    public abstract void Reload();
}
