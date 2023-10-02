using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieScreen : Screenn
{
    [SerializeField] private Camera _camera;

    private SecondLiveButton _secondLiveButton;
    private Button _button;

    private void Start()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }

    private void Awake()
    {
        _secondLiveButton = GetComponentInChildren<SecondLiveButton>();
    }

    public override void Close()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0f;
        Time.timeScale = 1f;
    }

    public override void Open()
    {
        Button button = _secondLiveButton.GetComponent<Button>();

        if (Random.Range(0, 100) <= 30)
            button.gameObject.SetActive(true);
        else
            button.gameObject.SetActive(false);

        _camera.GetComponent<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }
}
