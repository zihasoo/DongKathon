using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody characterRigidbody;

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)){
            characterRigidbody.AddForce(-speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow)){
           characterRigidbody.AddForce(speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow)){
            characterRigidbody.AddForce(0, 0, speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            characterRigidbody.AddForce(0, 0, -speed);
        }
    }
}
