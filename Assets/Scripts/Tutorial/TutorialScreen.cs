using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialScreen : Screenn
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private AdditionalMenus _shoot;
    [SerializeField] private PauseScreen _pause;

    private bool _isUseButton = false;

    private void Start()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }

    public override void Close()
    {
        _pause.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0f;
        _mainCamera.GetComponent<FirstPersonLook>().enabled = true;
        Time.timeScale = 1f;
    }

    public override void Open()
    {
        _pause.gameObject.SetActive(false);

        if(_isUseButton == false)
        {
            _shoot.Open();
            _isUseButton = true;
        }

        _mainCamera.GetComponent<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }
}
