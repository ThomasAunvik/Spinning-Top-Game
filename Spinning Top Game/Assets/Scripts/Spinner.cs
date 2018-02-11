using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Spinner : MonoBehaviour
{
    [SerializeField] private Transform respawnPosition;

    private Rigidbody rigidBody;

    [SerializeField]
    private Slider velocitySlider;

    [SerializeField]
    private Vector3 centerOfMass;

    private bool launched;
    public bool IsLaunched { get { return launched; } }

    private bool landed = false;

    private float collectedCoinsTimeReset = 0;

    [SerializeField]
    private Text coinText;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        rigidBody.centerOfMass = centerOfMass;

        isVelocityZero = false;
        rigidBody.isKinematic = true;

        transform.position = respawnPosition.position;
    }

    bool isVelocityZero;
    float highestVelocity;
    void Update()
    {
        if (GameManager.Instance) coinText.text = GameManager.Instance.GetCoins.ToString();
        else coinText.text = "0";

        if (!launched) highestVelocity = 0.01f;

        float smoothDampVelocity = 0;
        if (rigidBody.angularVelocity.y > highestVelocity) highestVelocity = rigidBody.angularVelocity.y;
        velocitySlider.value = Mathf.SmoothDamp(velocitySlider.value, rigidBody.angularVelocity.y / highestVelocity, ref smoothDampVelocity, 0.1f);

        if (Mathf.Round(rigidBody.angularVelocity.y) == 0 && !isVelocityZero && launched && landed)
        {
            isVelocityZero = true;
            Invoke("Respawn", 5);
        }

        if (transform.position.y <= -50) { landed = false; Respawn(); }
    }

    public void Launch(float velocity, Vector3 direction)
    {
        rigidBody.isKinematic = false;
        
        rigidBody.AddTorque(new Vector3(0, velocity * 100, 0));
        rigidBody.AddForce(direction * 2000);

        isVelocityZero = false;
        launched = true;

    }

    void Respawn()
    {
        if (landed && GameManager.Instance) GameManager.Instance.AddCoins(Mathf.RoundToInt(Time.timeSinceLevelLoad - collectedCoinsTimeReset));

        collectedCoinsTimeReset = Time.timeSinceLevelLoad;

        highestVelocity = 0.01f;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        rigidBody.isKinematic = true;
        launched = false;
        landed = false;

        transform.eulerAngles = new Vector3(-90, 0, 0);
        rigidBody.position = respawnPosition.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Arena>())
        {
            landed = true;
        }
    }
}
