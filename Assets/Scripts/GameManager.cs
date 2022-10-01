using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton, input for the pause 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    public int initialLifes = 3;

    public int currentLifes;

    public int score { get; private set; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        currentLifes = initialLifes;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }    

    public void RemoveLife()
    {
        currentLifes--;
        if (currentLifes == 0)
            endGame();
    }

    void endGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ToDo: add Properties AND Events
    #region timeControl

    public void StopTime()
    {

    }

    public void ContinueTime()
    {

    }

    public void StopInputs()
    {

    }

    public void ContinueInputs()
    {

    }
    #endregion
}
