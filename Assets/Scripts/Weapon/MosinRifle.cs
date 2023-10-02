using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MosinRifle : Weapon
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _zoomCamera;
    [SerializeField] private TouchController _touchController;
    [SerializeField] private CanvasGroup _buttons;
    [SerializeField] private Image _zoomButton;

    private WaitForSeconds _delayShoot = new WaitForSeconds(1.5f);
    private WaitForSeconds _delayBanShoot = new WaitForSeconds(1f);
    private Coroutine _BanShoot;
    private bool _isZoom = false;
    private bool IsDelaying = false;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Game.IsMobile == false && IsShooting == false && IsReloading == false)
        {
            ShootCoroutine = StartCoroutine(ShootDelay());
        }

        if (ShootButton.IsHold && Game.IsMobile && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            ShootCoroutine = StartCoroutine(ShootDelay());
        }

        if (Input.GetButtonDown("Fire2") && Game.IsMobile == false)
        {
            Zoom();
        }

        if (Input.GetKeyDown(KeyCode.R) && IsReloading == false)
        {
            Reload();
        }

        CurrentBullets.text = CountBulletSpent.ToString();
        MaxBullets.text = MaxBulletCount.ToString();
    }

    private void OnEnable()
    {
        IsReloading = false;
        IsDelaying = false;
        _BanShoot = StartCoroutine(BanShoot());
        _zoomButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        IsDelaying = false;
        IsShooting = false;
        _zoomButton.gameObject.SetActive(false);
    }

    public override void Reload()
    {
        if (gameObject.activeSelf && IsReloading == false)
        {
            IsReloading = true;
            ReloadCoroutine = StartCoroutine(ReloadDelay());
        }
    }

    public override void Shoot()
    {
        CountBulletSpent--;

        RaycastHit hit;

        AudioManager.instance.Play("MosinShoot");

        Animator.SetTrigger("shoot");

        if (Physics.Raycast(base.MainCamera.transform.position, base.MainCamera.transform.forward, out hit, Range) && _isZoom == false)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Explosion explosion = hit.transform.GetComponent<Explosion>();
            Head head = hit.transform.GetComponent<Head>();

            if (head != null)
                enemy = head.GetComponentInParent<Enemy>();


            if (enemy != null && head == null && enemy.IsDied == false)
            {
                enemy.TakeDamage(Damage, "body");

                _player.Aim.DOColor(Color.red, 0.05f);
                _player.Aim.DOColor(Color.black, 0.4f);
            }

            if (head != null && enemy.IsDied == false)
            {
                head.GetComponentInParent<Enemy>().TakeDamage(Damage * 2,"head");

                _player.Aim.DOColor(Color.red, 0.05f);
                _player.Aim.DOColor(Color.black, 0.4f);
            }

            if (explosion != null)
            {
                explosion.ExplodeDelay();
            }
        }

        else if (Physics.Raycast(_zoomCamera.transform.position, _zoomCamera.transform.forward, out hit, Range * 100f) && _isZoom)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Explosion explosion = hit.transform.GetComponent<Explosion>();
            Head head = hit.transform.GetComponent<Head>();

            if (head != null)
                enemy = head.GetComponentInParent<Enemy>();


            if (enemy != null && head == null && enemy.IsDied == false)
            {
                enemy.TakeDamage(Damage, "body");

                _player.Aim.DOColor(Color.red, 0.05f);
                _player.Aim.DOColor(Color.black, 0.4f);
            }

            if (head != null && enemy.IsDied == false)
            {
                head.GetComponentInParent<Enemy>().TakeDamage(Damage * 2, "head");

                _player.Aim.DOColor(Color.red, 0.03f);
                _player.Aim.DOColor(Color.black, 0.4f);
            }

            if (explosion != null)
            {
                explosion.ExplodeDelay();
            }
        }

        IsShooting = false;
    }

    private IEnumerator BanShoot()
    {
        IsShooting = true;
        yield return _delayBanShoot;
        IsShooting = false;
        StopCoroutine(_BanShoot);
    }

    private IEnumerator ShootDelay()
    {
        IsShooting = true;

        if (IsDelaying) yield break;

        IsDelaying = true;

        if (CountBulletSpent > 0)
        {
            Animator.SetTrigger("shoot");
            AudioManager.instance.Play("MosinShoot");
            ShootEffect.Play();
            Shoot();
            yield return _delayShoot;
        }
        else if (CountBulletSpent <= 0)
        {
            Reload();
        }

        IsShooting = false;
        IsDelaying = false;
    }

    private IEnumerator ReloadDelay()
    {
        Animator.SetTrigger("reload");

        AudioManager.instance.Play("ReloadMosin");

        CountBulletSpent = MaxBulletCount;
        yield return DelayReload;
        IsReloading = false;
    }

    public void Zoom()
    {
        if (gameObject.activeSelf)
        {

            if (_isZoom == false)
            {
                OnZoom();
                _buttons.alpha = 0f;
            }

            else if (_isZoom)
            {
                OffZoom();
                _buttons.alpha = 1f;
            }
        }
    }

    private void OnZoom()
    {
        _isZoom = true;
        _zoomCamera.gameObject.SetActive(true);

        if (MainCamera != null)
            MainCamera.enabled = false;

        if (Game.IsMobile == false)
            MainCamera.GetComponent<FirstPersonLook>().ChangeSensivity(_isZoom);
        else if (_touchController != null)
            _touchController.ChangeSensitivity(_isZoom);
    }

    private void OffZoom()
    {
        _isZoom = false;
        _zoomCamera.gameObject.SetActive(false);

        if(MainCamera != null)
        MainCamera.enabled = true;

        if (Game.IsMobile == false)
            MainCamera.GetComponent<FirstPersonLook>().ChangeSensivity(_isZoom);
        else if(_touchController!= null)
            _touchController.ChangeSensitivity(_isZoom);
    }
}
