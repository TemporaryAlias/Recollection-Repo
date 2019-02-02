using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour {

    //Create an instance of the scene manager so it can be referenced from anywhere
    public static LevelManager _instance = null;

    [Header("Game Variables")]
    public string gameState;
    public int currentScore;
    public bool peeking;
    public float cameraFOV = 70;

    [Space(5)]

    [Header("Player Variables")]
    public GameObject playerPrefab;
    public GameObject player;

    PlayerSpawn playerSpawnPoint;

    UIHandler uiHandler;

    //Generate scene manager, make sure this is the only one. Ensure it isn't removed on scene load
    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        //Get UI handler
        uiHandler = FindObjectOfType<UIHandler>();

        //Check new scenes name, if it contains word "menu", then use menu game state. Otherwise, use platformer
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name.ToLower().Contains("menu")) {
            ChangeState("Menu");
            player = null;
        } else {
           SpawnPlayer();
        }

        //Add OnSceneLoad to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Get UI handler
        uiHandler = FindObjectOfType<UIHandler>();

        //Check new scenes name, if it contains word "menu", then use menu game state. Otherwise, use platformer
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name.ToLower().Contains("menu")) {
            ChangeState("Menu");
        } else {
            SpawnPlayer();
        }
    }

    public void ChangeState(string newState) {
        //Change the game state to the new one specified in the constructor
        gameState = newState;

        

        //If not menu, handle the player
        if (gameState != "Menu") {
            //Get the player mover
            Movement playerMover = player.GetComponent<Movement>();
            Animator playerAnim = player.GetComponent<Animator>();

            //If the gamestate is platformer, player can move around. Otherwise, they should remain still
            if (gameState == "Platformer") {
                playerMover.enabled = true;
                //Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);

                CameraFocusPlayer();

                //Change UI accordingly
                uiHandler.platformerUI.SetActive(true);
                uiHandler.minigameUI.SetActive(false);
            } else {
                //If not platformer, freeze player and change UI accordingly
                playerMover.SetVelocity(Vector2.zero);
                playerMover.enabled = false;
                playerAnim.SetBool("isWalking", false);

                uiHandler.minigameUI.SetActive(true);
                uiHandler.platformerUI.SetActive(false);
            }
        }
    }

    public void ChangeSceneByIndex(int nextSceneIndex) {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void SpawnPlayer()  {
        //Find a player spawn point on the map, and spawn them at it
        playerSpawnPoint = FindObjectOfType<PlayerSpawn>();

        Destroy(player);

        player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
        playerSpawnPoint.gameObject.SetActive(false);

        Player playerObj = player.GetComponent<Player>();

        playerObj.healthText = uiHandler.healthText;

        //Default gamestate set
        ChangeState("Platformer");
    }

    public void SetCameraFocus(GameObject newFocus) {
        Camera.main.transform.position = new Vector3(newFocus.transform.position.x, newFocus.transform.position.y, Camera.main.transform.position.z);

        CinemachineVirtualCamera vcam = Camera.main.gameObject.GetComponent<CinemachineVirtualCamera>();
        vcam.m_Follow = newFocus.transform;

        //Camera.main.transform.SetParent(newFocus.transform);
    }

    public void CameraFocusPlayer() {
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);

        Camera.main.fieldOfView = cameraFOV;

        CinemachineVirtualCamera vcam = Camera.main.gameObject.GetComponent<CinemachineVirtualCamera>();
        vcam.m_Follow = player.transform;
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
