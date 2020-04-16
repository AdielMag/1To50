using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using GameAnalyticsSDK;


public class Logo : MonoBehaviour
{

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2)
        {
            GameManager.instance.LoadScene("Game5X5");
            enabled = false;
        }
    }
}
