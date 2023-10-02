using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private ParticleSystem _boomEffect;
    [SerializeField] private bool _isActive = false;
    [SerializeField] private AudioClip _boomClip;

    private Player _player;
    private AudioSource _audioSourse;
    private bool _explosionDone = false;
    private float _damage = 70f;

    private void Start()
    {
        _audioSourse= GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isActive)
        {
            ExplodeDelay();
        }
    }

    private void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;

            if (rigidbody != null)
            {
                Explosion explosion = rigidbody.GetComponent<Explosion>();
                Enemy enemy = rigidbody.gameObject.GetComponent<Enemy>();

                rigidbody.AddExplosionForce(_force, transform.position, _radius, 1f);

                if(explosion != null)
                {
                    if (Vector3.Distance(transform.position, rigidbody.position) < _radius / 2f)
                    {
                        explosion.ExplodeDelay();
                    }
                }

                if(enemy != null)
                {
                    _player = enemy.Player;

                    if (Vector3.Distance(transform.position, rigidbody.position) < _radius / 2f)
                    {
                        enemy.TakeDamage(_damage * 2, "head");
                    }
                    else
                    {
                        enemy.TakeDamage(_damage, "body");                  
                    }
                }
            }
        }

        _audioSourse.PlayOneShot(_boomClip);
        Instantiate(_boomEffect, transform.position, Quaternion.identity);

        Invoke("DestroyObject", 3f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void ExplodeDelay()
    {
        if(_explosionDone) return;
        _explosionDone = true;

        Invoke("Explode", 0.5f);
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);

       Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius / 2);
    }
}
