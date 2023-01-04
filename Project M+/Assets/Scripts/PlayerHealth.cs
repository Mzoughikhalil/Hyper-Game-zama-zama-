using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameOver gameOverHundler;
    public void Crash()
    {
        gameOverHundler.EndGame();
        gameObject.SetActive(false);
    }
    
}
