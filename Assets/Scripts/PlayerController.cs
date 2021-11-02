using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winOrLoseText;
    public GameObject timer;
    public int count;
    public Text countdownTextGameStart;
    public bool countdownRunning = true;

    private Rigidbody rb;
    private Timer _timerScript;

    void Start ()
    {
        StartCoroutine(Countdown(3));
        StartGame();

    }


    void FixedUpdate ()
    {
        //if (!countdownRunning)
        //{
            //wenn timer läuft (fängt an zu laufen wenn startcountdown abgelaufen ist--> für 3, 2, 1 also false)
            if(_timerScript.timerIsRunning){
                if (count < 12)
                {
                     SetPlayerMovement();
                } else {

                }
            } else if (_timerScript.timerEnded){
                winOrLoseText.text = "You Lose This Time... Try Again!";       
            }
        //} else {}
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
       
        while (count > 0) {
            countdownTextGameStart.text = count.ToString();
            yield return new WaitForSeconds(1);
            count --;
        }
        countdownTextGameStart.text ="";
        countdownRunning = false;
    }

    void SetCountText ()
    {
        countText.text = count.ToString ();
        if (count >= 12)
        {
            winOrLoseText.text = "You Win!";
        }
    }

    void StartGame()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winOrLoseText.text = "";
        
        _timerScript = timer.GetComponent<Timer>();
    }

    void SetPlayerMovement ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }

}