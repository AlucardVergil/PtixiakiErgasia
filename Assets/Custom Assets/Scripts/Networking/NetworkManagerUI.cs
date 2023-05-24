using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NetworkManagerUI : MonoBehaviour
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
        yield return new WaitForSeconds(1);

        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));

        SceneManager.LoadScene("GameScene");
    }

}
