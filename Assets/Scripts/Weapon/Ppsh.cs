using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Ppsh : Weapon
{
    [SerializeField] private Player _player;

    private WaitForSeconds _delayShoot = new WaitForSeconds(0.1f);

    private void Update()
    {
        if (Input.GetButton("Fire1") && Game.IsMobile == false && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            StartCoroutine(ShootDelay());
        }

        if (ShootButton.IsHold && Game.IsMobile && IsShooting == false && IsReloading == false)
        {
            IsShooting = true;
            StartCoroutine(ShootDelay());
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
        IsShooting = false;
        IsReloading = false;
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

        if (CountBulletSpent >= 0)
        {

            RaycastHit hit;

            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit, Range))
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
                    head.GetComponentInParent<Enemy>().TakeDamage(Damage, "head");

                    _player.Aim.DOColor(Color.red, 0.05f);
                    _player.Aim.DOColor(Color.black, 0.4f);
                }

                if (explosion != null)
                {
                    explosion.ExplodeDelay();
                }
            }
        }
        else if (CountBulletSpent < 0)
        {
            Reload();
        }

        IsShooting = false;
    }
    private IEnumerator ShootDelay()
    {
        yield return _delayShoot;
        IsShooting = false;
        AudioManager.instance.Play("PpshShoot");
        Animator.SetTrigger("shoot");
        ShootEffect.Play();
        Shoot();

    }

    private IEnumerator ReloadDelay()
    {
        Animator.SetTrigger("reload");

        AudioManager.instance.Play("ReloadPpsh");

        CountBulletSpent = MaxBulletCount;
        yield return DelayReload;
        IsReloading = false;
    }
}
