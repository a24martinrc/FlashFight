using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Presionar "Enter" en StartScene para ir a GameScene
        if (SceneManager.GetActiveScene().name == "StartScene" && Input.GetKeyDown(KeyCode.Return))
        {
            LoadGameScene();
        }

        // Presionar "Esc" en cualquier escena para volver a StartScene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadStartScene();
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
