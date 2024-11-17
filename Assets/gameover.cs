using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public void restartButton(){
        SceneManager.LoadScene("Main_Scene");
    }

    public void quitButton(){
        SceneManager.LoadScene("Menu");
    }
}
