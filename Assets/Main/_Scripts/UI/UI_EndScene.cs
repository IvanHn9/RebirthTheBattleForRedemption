using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndScene : MonoBehaviour
{
    [SerializeField] UI_FadeScreen fadeScreen;
    public void ToMainMenu()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1f));
    }
    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(1);
        SaveManager.instance.LoadGame();
        //SceneManager.LoadSceneAsync(sceneName);

    }
}
