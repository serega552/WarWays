using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private EnemyController[] _enemyControllers;
    [SerializeField] private Sniper[] _snipers;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private bool _isTutorial;

    private int _countEnemy;

    public PlayerController PlayerController => _playerController;
    public int CountEnemy => _countEnemy;

    private void Start()
    {
        foreach(var item in _enemyControllers)
        {
            _countEnemy++;
        }

        foreach(var item in _snipers)
        {
            _countEnemy++;
        }
    }

    public void LetEnemiesAttack()
    {
        if (_isTutorial == false)
        {
            foreach (var item in _enemyControllers)
            {
                item.Move();
                item.GetComponent<Animator>().SetBool("Run", true);
            }

            foreach (var item in _snipers)
            {
                if(item.gameObject.active == true)
                item.Attack();
            }
        }
    }

    public void Clear()
    {
        foreach (var item in _enemyControllers)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in _snipers)
        {
            if (item.gameObject.active == true)
                item.gameObject.SetActive(false);
        }

        _countEnemy = 0;
        _playerController.Continue();
    }

    public void DieEnemy()
    {
        _countEnemy--;

        if (_countEnemy <= 0 && _playerController.transform.position == transform.position)
            _playerController.Continue();
    }
}
