using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
public void PlayGame()
{
    SceneManager.LoadScene("BenTestWorld"); 
}

public void QuitGame(){
	Application.Quit();

}

public void MoveToMenu()
{
    SceneManager.LoadScene("MainMenu"); 
}

public void Credits()
{
    SceneManager.LoadScene("Credits"); 
}

public void Options()
{
    SceneManager.LoadScene("OptionsMenu(Home)"); 
}
}
