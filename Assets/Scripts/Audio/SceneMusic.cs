using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private string _sceneMusic;

    private void Awake()
    {
        AudioManager.instance.Play(_sceneMusic);
    }
}
