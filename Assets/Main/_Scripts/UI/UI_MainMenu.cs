using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour//,ISaveManager
{
    [SerializeField] private string sceneName= "MainScene1";
    [SerializeField] private string scene1Name = "MainScene1";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start()
    {
        //if (SaveManager.instance.HasSavedData() == false)
            //continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1f, sceneName));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(1f,scene1Name));
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay,string _sceneName)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

       SceneManager.LoadScene(_sceneName);
       SaveManager.instance.LoadGame();
        //SceneManager.LoadSceneAsync(sceneName);
        
    }
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(7, null);
    }

    //public void LoadData(GameData _data)
    //{
    //    if (_data.sceneName != string.Empty)
    //    {
    //        sceneName = _data.sceneName;
    //    }
    //    else
    //        sceneName = scene1Name;
    //}

    //public void SaveData(ref GameData _data)
    //{
    //    Debug.Log("save");
    //}
}
