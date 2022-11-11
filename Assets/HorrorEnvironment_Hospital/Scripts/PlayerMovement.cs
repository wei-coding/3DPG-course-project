using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSprintSpeed;
    public float maxWalkSpeed;
    private float moveSpeed;

    public float staticDrag;

    [Header("Ground Check")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    Animator player;

    Vector3 idleCamPos;
    Vector3 movingCamPos;
    bool isFirstPerson = true;

    GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        player = GetComponentInChildren<Animator>();
        idleCamPos = new Vector3(0.15f, 1.296f, 0.303f);
        movingCamPos = new Vector3(0.078f, 0.843f, 0.423f);
        mainCamera = GameObject.Find("Main Camera");
        moveSpeed = maxWalkSpeed;
    }

    void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer() {
        moveDirection = orientation.right * verticalInput - orientation.forward * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f);

        if (verticalInput == 0 && horizontalInput == 0) {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            player.SetBool("walking", false);
            
        } else {
            rb.useGravity = true;
            player.SetBool("walking", true);
            
        }
    }

    void SpeedControl() {
        Vector3 flatVal = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVal.magnitude > moveSpeed) {
            Vector3 limitedVal = flatVal.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVal.x, rb.velocity.y, limitedVal.z);
        }
        
        if (rb.velocity.y > 0) {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    void CheckCrouch() {
        if (Input.GetKey(KeyCode.LeftControl)) {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, movingCamPos, 20 * Time.deltaTime);
            player.SetBool("crouch", true);
        } else {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, idleCamPos, 20 * Time.deltaTime);
            player.SetBool("crouch", false);
        }
    }

    void CheckSprint() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = maxSprintSpeed;
        } else {
            moveSpeed = maxWalkSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCrouch();
        CheckSprint();
        MyInput();
    }

    void FixedUpdate() {
        SpeedControl();
        MovePlayer();
    }
}
