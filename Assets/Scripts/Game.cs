using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;
using Scene = UnityEngine.SceneManagement.Scene;

public class Game : MonoBehaviour
{
    [SerializeField] private string _sceneMusic;
    [SerializeField] private GameObject _control;

    private bool _isMobile;
    private Scene _scene;
    private GameUI _gameUI;

    public bool IsMobile => _isMobile;

    private void Start()
    {
        if (PlayerPrefs.GetInt("platform") == 1)
        {
            _isMobile = true;
        }
        else
        {
            _isMobile = false;
            _control.SetActive(false);
        }

        _gameUI = GetComponentInChildren<GameUI>();

        _scene = SceneManager.GetActiveScene();

        AudioManager.instance.Play(_sceneMusic);
    }

    public void Restart()
    {
        SceneManager.LoadScene(_scene.name);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StopMusic();
        AudioListener.volume = PlayerPrefs.GetFloat("SoundSettings");
        MenuScene.Load();
    }

    public void WinLevel()
    {
        _gameUI.Win();
    }

    private void StopMusic()
    {
        AudioManager.instance.Stop(_sceneMusic);
        AudioManager.instance.Stop("Rain");
        AudioManager.instance.Stop("Run");
    }
}
