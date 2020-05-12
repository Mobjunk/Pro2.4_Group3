using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeModeManager : MonoBehaviour
{
    public float gameTimer;
    public Text gameTimerText;
    [SerializeField] TimerChanger timeChangerRef;
    // Start is called before the first frame update
    void Start()
    {
        timeChangerRef = new TimerChanger();
        gameTimer = timeChangerRef.StartTime();
        
    }
    
 
    // Update is called once per frame
    void Update()
    {
        gameTimer = timeChangerRef._timeLeft;
        gameTimerText.text = gameTimer.ToString("F0");
       
    }
    
}
