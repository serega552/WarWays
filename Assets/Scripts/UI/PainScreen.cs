using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainScreen : Screenn
{
    private Coroutine _changeAlpha;
    private bool _isOpen = false;

    private void Start()
    {
        CanvasGroup.alpha = 0f;
    }

    public override void Close()
    {
        StopCoroutine(_changeAlpha);
    }

    public override void Open()
    {
        _changeAlpha = StartCoroutine(ChangeAlpha());
    }

    private IEnumerator ChangeAlpha()
    {
        while (CanvasGroup.alpha <= 1f && _isOpen == false)
        {
            yield return new WaitForSeconds(0.01f);
            CanvasGroup.alpha += 0.1f;

            if (CanvasGroup.alpha >= 0.9f)
                _isOpen = true;
            yield return null;
        }

        while (CanvasGroup.alpha >= 0f && _isOpen)
        {
            yield return new WaitForSeconds(0.1f);
            CanvasGroup.alpha -= 0.1f;

            if (CanvasGroup.alpha <= 0f)
            {
                Close();
                _isOpen = false;
            }
            yield return null;
        }

    }
}
