using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageText : MonoBehaviour
{
    public int Language;
    public string[] _text;
    private TMP_Text _textLine;

    void Start()
    {
        Language = PlayerPrefs.GetInt("language", Language);
        _textLine = GetComponent<TMP_Text>();
        _textLine.text = "" + _text[Language];
    }
}