using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public abstract class Screenn : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup CanvasGroup;

        public abstract void Open();

        public abstract void Close();
    }
