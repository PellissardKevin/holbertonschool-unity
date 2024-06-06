using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    private int score = 0;
    private int health = 5;
    public Text scoreText;
    public Text healthText;
    public Image winLoseBG;
    public Text winLoseText;
    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        SetScoreText();
        SetHealthText();
        winLoseBG.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerBody.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score++;
            // Debug.Log("Score: " + score);
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.tag == "Trap")
        {
            health--;
            SetHealthText();
            // Debug.Log("Health: " + health);
        }

        if (other.tag == "Goal")
        {
            // Debug.Log("You win!");
            winLoseBG.gameObject.SetActive(true);
            winLoseBG.color = Color.green;

            winLoseText.text = "You Win!";

            winLoseText.color = Color.black;
            StartCoroutine(LoadScene(3));

        }

        if (other.tag == "Teleporter")
        {
            Debug.Log("Teleporte player");
        }
    }

    void Update()
    {
        if (health == 0)
        {
            //Debug.Log("Game Over!");
            winLoseBG.gameObject.SetActive(true);
            winLoseBG.color = Color.red;

            winLoseText.text = "Game Over!";

            winLoseText.color = Color.white;
            StartCoroutine(LoadScene(3));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the menu scene
            SceneManager.LoadScene("menu");
        }
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
        Debug.Log(scoreText);
    }
    void SetHealthText()
    {
        healthText.text = "Health: " + health;
        Debug.Log(healthText);
    }
}
