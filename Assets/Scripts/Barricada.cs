using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricada : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            _playerController.UsingShelter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _playerController.UnUsingShelter();
    }
}
