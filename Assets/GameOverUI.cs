using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI roundSurvivedText;
    public TextMeshProUGUI zombiesKilledText;
    private int numOfZombiesKilled;
    private int numOfRoundsSurvived;
    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatsCollection").GetComponent<Stats>();

        numOfRoundsSurvived = stats.numOfRoundsSurvived;
        numOfZombiesKilled = stats.numOfZombiesKilled;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        
        roundSurvivedText.text = "You Survived " + numOfRoundsSurvived + " Rounds!";
        zombiesKilledText.text = "You Killed " + numOfZombiesKilled + " Zombies..."; 
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
