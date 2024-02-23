using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    private Transform player;

    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private string closestCheckpointId;


    public bool pausedGame { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Start()
    {
       
        checkpoints = FindObjectsOfType<Checkpoint>();

        player = PlayerManager.instance.player.transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            RestartScene();

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!pausedGame)
            {
                pausedGame = true;
                PauseGame(pausedGame);
            }
            else
            {
                pausedGame = false;
                PauseGame(pausedGame);
            }

        }
    }
    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void ExitScene()
    {
        SaveManager.instance.SaveGame();
        PauseGame(false);
        SceneManager.LoadScene(0);
    }
    public void GoToNextScene()
    {
        SaveManager.instance.SaveGame();
        UI.instance.fadeScreen.FadeOut();
        StartCoroutine(LoadSceneWithFadeEffect(1,SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator LoadSceneWithFadeEffect(float _delay,int index)
    {
        
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(index);
        SaveManager.instance.LoadGame();

    }
    
    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }
    }
    public void LoadData(GameData _data)
    {
        StartCoroutine(LoadWithDelay(_data));
    }

    private IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(0.1f);

        LoadCheckpoints(_data);
        LoadClosestCheckpoint(_data);
        FindObjectOfType<Arena>().GetComponent<Arena>().openDoor();
    }
    public void SaveData(ref GameData _data)
    {
        _data.sceneName = SceneManager.GetActiveScene().name;
        
        if (FindClosestCheckpoint() != null)
            _data.closestCheckpointId = FindClosestCheckpoint().id;

        _data.checkpoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.id, checkpoint.activationStatus);
        }
    }
    private void LoadClosestCheckpoint(GameData _data)
    {
        if (_data.closestCheckpointId == null)
            return;


        closestCheckpointId = _data.closestCheckpointId;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (closestCheckpointId == checkpoint.id)
                player.position = checkpoint.transform.position;
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.activationStatus == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }


    public void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}