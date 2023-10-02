using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : Screenn
{
    [SerializeField] private Camera _camera;

    private bool _isPause = false;

    private void Start()
    {
        CanvasGroup.alpha= 1f;
        CanvasGroup.blocksRaycasts = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPause == false)
        {
            Open();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _isPause == true))
        {
            Close();
        }

        if (_isPause)
        {
            Time.timeScale = 0f;
            AudioListener.volume = 0;
        }
    }

    protected void OnButtonClick()
    {
        if(_isPause == false)
        {
            Open();
        }
        else if(_isPause)
        {
            Close();
        }
    }

    public override void Open()
    {
       _camera.GetComponent<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;
        _isPause = true;
        Time.timeScale = 0f;
        AudioListener.volume = 0;
    }

    public override void Close()
    {
        _camera.GetComponent<FirstPersonLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0f;
        _isPause = false;
        Time.timeScale = 1f;
        AudioListener.volume = PlayerPrefs.GetFloat("SoundSettings");
    }
}