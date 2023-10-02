using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : Screenn
{
    private void Start()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }

    public override void Open()
    {
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;

        AudioManager.instance.Play("Click");
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }
}
