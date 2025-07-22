using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float defaultSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashCoolTime;
    public float rotationSpeed;

    private Vector3 moveDirection;

    private float lastDashTime;
    private bool isDashing;

    void Start()
    {
        isDashing = false;
        lastDashTime = -100;
    }

    void Update()
    {
        processMove();
        processDash();
    }

    void processMove()
    {
        transform.Translate(moveDirection * (Time.deltaTime * defaultSpeed), Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void processDash()
    {
        if (isDashing)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * dashSpeed));
        }
    }

    IEnumerator endDash()
    {
        yield return new WaitForSeconds(dashDuration); //�뽬 ���ӽð���ŭ ��ٷȴٰ� ����
        lastDashTime = Time.time;
        isDashing = false;
    }

    void OnMove(InputValue value) {
        Vector2 inputVec2 = value.Get<Vector2>();

        if (inputVec2 != null)
        {
            moveDirection = new Vector3(inputVec2.x, 0, inputVec2.y);
        }
    }

    void OnDash()
    {
        if (!isDashing && lastDashTime + dashCoolTime < Time.time)
        //�뽬 ���� �ƴϰ� (������ �뽬 �ð� + �뽬 ��Ÿ��)�� ������ ��
        {
            isDashing = true;
            StartCoroutine(endDash());
        }
    }
}
