using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float powerupStrength = 15.0f;
    private Rigidbody playerRb;
    public GameObject focalPoint;
    public GameObject powerupIndicator;
    public bool activePowerup = false;
    string textPowerup;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * playerSpeed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    IEnumerator PowerUpDisable()
    {
        yield return new WaitForSeconds(7);
        activePowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            activePowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpDisable());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(activePowerup)
        {
            textPowerup = "active";
        } else if (!activePowerup)
        {
            textPowerup = "inactive";
        }

        if (activePowerup && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision with: " + collision.gameObject.name + " while powerup is " + textPowerup);

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(awayfromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
