using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Knife : MonoBehaviour {
    public Rigidbody rb;
    public float force = 5f;
    public float torque = 20f;
    public GameObject restartButton;
    public TextMeshProUGUI flipCountText;
    private int flipCount;
    private float timeCalcuStart;
    private Vector2 startSwipe;
    private Vector2 endSwipe;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
	}
    void Swipe()
    {
        ++flipCount;
        flipCountText.SetText(flipCount.ToString());
        timeCalcuStart = Time.time;
        rb.isKinematic = false;
        Vector2 swipe = endSwipe - startSwipe;
        rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddTorque(0f,0f,torque,ForceMode.Impulse);
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Block")
        {
            rb.isKinematic = true;
        }
        else
        {
            ShowRestart();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        float timeInAir = Time.time - timeCalcuStart;
        if (!rb.isKinematic && timeInAir >= .025f)
        {
            ShowRestart();
        }
    }
    void ShowRestart()
    {
        Time.timeScale = 0f;
        restartButton.SetActive(true);
    }
    public void Restart()
    {
        flipCount = 0;
        flipCountText.SetText(flipCount.ToString());
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
