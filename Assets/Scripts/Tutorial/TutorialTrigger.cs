using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TutorialScreen _tutorialMobileScreen;
    [SerializeField] private TutorialScreen _tutorialDecktopScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(TurnOnTutorial());
        }
    }

    private IEnumerator TurnOnTutorial()
    {
        yield return new WaitForSeconds(1f);

        if (PlayerPrefs.GetInt("platform") == 1)
            _tutorialMobileScreen.Open();
        else
            _tutorialDecktopScreen.Open();
    }
}
