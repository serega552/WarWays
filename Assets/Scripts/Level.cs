using Agava.YandexGames;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private int _scoreUnlock;
    [SerializeField] private LevelsManager _levelsManager;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private TMP_Text _scoreUnlockBanner;

    private CanvasGroup _canvasGroup;
    private AdditionalMenus _additionalMenu;
    private PlayerStatistics _localPlayerData;
    private Button _button;
    private int _score;
    private int _newScore;
    private bool _isUnlock = false;
    private string _scoreLockName = "scoreLock";
    private string _scoreCountName = "scoreCount";
    private bool _isActiveEffect = false;

    private void Awake()
    {
        _additionalMenu = GetComponentInParent<AdditionalMenus>();
        _canvasGroup = _additionalMenu.GetComponent<CanvasGroup>();
        _button = GetComponent<Button>();

        _score = PlayerPrefs.GetInt(_sceneName + _scoreCountName);

        if(_scoreUnlock == 0)
        {
            _isUnlock = true;
            _button.interactable = true;
        }

        if(_isUnlock == false)
        {
            _button.interactable = false;
            _scoreUnlockBanner.text = _scoreUnlock.ToString();
            _scoreUnlockBanner.gameObject.SetActive(true);
        }

        _localPlayerData = GlobalControl.Instance.SavedPlayerData;

        if (_sceneName == _localPlayerData.SceneName)
        {
            _newScore = _localPlayerData.Score;

            if(_newScore >= _score)
            PlayerPrefs.SetInt(_sceneName + _scoreCountName, _newScore);
            _levelsManager.SumScore(PlayerPrefs.GetInt(_sceneName + _scoreCountName));
        }

        UnlockLevel();

        SaveData();
    }

    private void Update()
    {
        if (_canvasGroup.alpha == 1)
        {
            StartCoroutine(AddStars());
            _isActiveEffect = true;
        }
    }

    private IEnumerator AddStars()
    {
        if (PlayerPrefs.HasKey("stars" + _sceneName))
        {
            if (PlayerPrefs.GetInt("stars" + _sceneName) == 1)
            {
                _stars[0].SetActive(true);
            }
            else if (PlayerPrefs.GetInt("stars" + _sceneName) == 2)
            {
                _stars[0].SetActive(true);
                yield return new WaitForSeconds(1.0f);
                _stars[1].SetActive(true);
            }
            else
            {
                _stars[0].SetActive(true);
                yield return new WaitForSeconds(1.0f);
                _stars[1].SetActive(true);
                yield return new WaitForSeconds(1.0f);
                _stars[2].SetActive(true);
            }
        }
    }

    public void UnlockLevel()
    {
        if (PlayerPrefs.GetInt(_sceneName + _scoreLockName) == 1)
        {
            _isUnlock = true;
            _button.interactable = true;

            _scoreUnlockBanner.gameObject.SetActive(false);
        }

        if (_levelsManager.TotalScore >= _scoreUnlock && _isUnlock == false)
        {
            _isUnlock = true;
            _button.interactable = true;
            PlayerPrefs.SetInt(_sceneName + _scoreLockName, 1);

            _scoreUnlockBanner.gameObject.SetActive(false);
        }
    }

    public void LoadLevel()
    {
        if (_isUnlock)
        {
            AudioManager.instance.Stop("MenuMusic");

            SceneManager.LoadScene(_sceneName);
            _localPlayerData.SceneName = _sceneName;
            SaveData();
        }
    }

    private void SaveData()
    {
        GlobalControl.Instance.SavedPlayerData = _localPlayerData;
    }
}
