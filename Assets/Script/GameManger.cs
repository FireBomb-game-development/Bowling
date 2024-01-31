using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{

    public GameObject ball;
    int sorce = 0;
    GameObject[] pins;
    public TextMeshProUGUI scoreUi;
    // Start is called before the first frame update
    Scene currentScene;
  
    Vector3 [] postions;

    public TextMeshProUGUI level;

    void Start()
    {
        Time.timeScale = 1.0f;

        currentScene = SceneManager.GetActiveScene();
        pins = GameObject.FindGameObjectsWithTag("pin");
        postions = new Vector3[pins.Length];
        for(int i = 0; i < pins.Length; i++)
        {
            postions[i]= pins[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Moveball();
        if(Input.GetKeyDown(KeyCode.Space)|| ball.transform.position.y<20) {

            countPinsDown();
            
              if (IsBallReadyForReset())
        {
            ResetPins();
        }


        }
        if(sorce >= 9 && currentScene.name == "level1")
        {
            level.text = "2";
            SceneManager.LoadScene("level2");
            Time.timeScale = 1.0f;
            Stats.turn = 0;
        }
        else if (Stats.turn == 2&& sorce <=9 && currentScene.name == "level1")
        {
            level.text = "1";
            Stats.turn = 0;
            SceneManager.LoadScene("level1");
        }
        else if (sorce == 15 && currentScene.name == "level2")
        {
            level.text = "3";

            SceneManager.LoadScene("level3");
            Time.timeScale = 1.0f;

            Debug.Log(currentScene.name);
        }
        else if (Stats.turn == 2 && sorce <= 14 && currentScene.name == "level2")
        {
            Stats.turn = 0;
            level.text = "2";
            SceneManager.LoadScene("level2");
            Time.timeScale = 1.0f;


        }
        else if (sorce == 15 && currentScene.name == "level3")
        {

            level.text = "4";

            SceneManager.LoadScene("level4");
            Time.timeScale = 1.0f;

            Debug.Log(currentScene.name);
        }
        else if (Stats.turn == 2 && sorce <= 14 && currentScene.name == "level3")
        {
            Stats.turn = 0;
            level.text = "3";

            SceneManager.LoadScene("level3");
            Time.timeScale = 1.0f;

            Debug.Log(currentScene.name);
        }
    }

     void Moveball()
    {
        Vector3 position = ball.transform.position;
        position += Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -0.525f, 0.525f);
        ball.transform.position = position;
       // ball.transform.Translate(Vector3.right * Input.GetAxis("Horizontal")*Time.deltaTime);
    }
    void countPinsDown()
    {
        for (int i = 0; i < pins.Length; i++) {
            if (pins[i].transform.eulerAngles.z>5 && pins[i].transform.eulerAngles.z<355&& pins[i].activeSelf) {
                sorce++;
                pins[i].SetActive(false);
            }
        }
      scoreUi.text= sorce.ToString();
    }

     void ResetPins()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true);
            pins[i].transform.position = postions[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation= Quaternion.identity;

        }

        ball.transform.position= new Vector3(0, 0.10905f, -8.65f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity= Vector3.zero;
        ball.transform.rotation= Quaternion.identity;
    }
    bool IsBallReadyForReset()
    {
        // Add conditions based on your game logic
        // For example, you might want to check if the ball is not moving or has stopped bouncing
        return ball.GetComponent<Rigidbody>().velocity.magnitude < 0.1f;
    }
}
