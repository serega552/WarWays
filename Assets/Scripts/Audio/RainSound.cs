using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RainSound : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("Rain");
    }
}
