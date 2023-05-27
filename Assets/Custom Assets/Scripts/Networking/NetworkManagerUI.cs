using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;


    private void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        hostBtn.onClick.AddListener(() =>
        {
            StartCoroutine(StartHostAndLoadGameScene());
        });

        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }


    IEnumerator StartHostAndLoadGameScene()
    {
        NetworkManager.Singleton.StartHost();
        yield return new WaitForSeconds(0.1f);

        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));

        NetworkManager.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

}
