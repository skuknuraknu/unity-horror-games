using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Movement Sound")]
    private AudioSource audioS;
    public AudioClip[] stepSounds;
    public float footStepRate, footStepTreshHold;
    private float lastStepTime;
    
    [Header("Jumping")]
    public float jumpForce;
    public LayerMask groundLayer;

    [Header("Movement")]
    public float moveSpeed = 6; // variabel untuk gerak
    private Vector2 currentMovementInput;

    [Header("Camera Look")]
    public Transform CameraContainer; // Akses ke nilai transform kamera kita
    public float minXlook,maxXlook; // variabel untuk liat kiri kanan
    private float camCurrentXRotation; // variabel untuk rotasi kamera
    public float lookSensitivity; // variabel untuk sensitivitas kamera
    private Vector2 mouseDelta;
    private Rigidbody myRig;
    public bool canLook = true;

    private void Awake(){
        instance = this;
        myRig = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
    }
    // start dipanggil sebelum update frame pertama
    private void start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate(){
        CameraLook();
    }
    // Fixedupdate will be called 50 time per second (around 0.02)
    private void FixedUpdate(){
        Move();
    }
    private void Move(){
        Vector3 moveDirection = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        moveDirection *= moveSpeed;
        moveDirection.y = myRig.velocity.y;
        myRig.velocity = moveDirection;

        if(moveDirection.magnitude > footStepTreshHold && IsGrounded()){
            if(Time.time  -lastStepTime > footStepRate){
                lastStepTime = Time.time;
                audioS.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
            }
        }
    }
    // context mengandung semua informasi yang kita butuhkan mengenai inputaction
    public void OnLookInput(InputAction.CallbackContext context){
        mouseDelta = context.ReadValue<Vector2>();
    }
    public void OnMoveInput(InputAction.CallbackContext context){
        // check if we pressing the button
       if(context.phase == InputActionPhase.Performed){
            currentMovementInput = context.ReadValue<Vector2>();
       }else if(context.phase == InputActionPhase.Canceled){
            currentMovementInput = Vector2.zero;
       }
    }
   private void CameraLook(){
        camCurrentXRotation += mouseDelta.y * lookSensitivity;
        camCurrentXRotation = Mathf.Clamp(camCurrentXRotation,minXlook, maxXlook);
        CameraContainer.localEulerAngles = new Vector3(-camCurrentXRotation, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
   }
   public void OnJumpInput(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started){
            if(IsGrounded()){
                myRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
   } 
   bool IsGrounded(){  
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward* 0.2f)+(Vector3.up * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.forward* 0.2f)+(Vector3.up * 0.2f), Vector3.down),
            new Ray(transform.position + (transform.right* 0.2f)+(Vector3.up * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.right* 0.2f)+(Vector3.up * 0.2f), Vector3.down),
        };
        for(int i = 0; i < rays.Length; i++){
            if(Physics.Raycast(rays[i], 0.1f, groundLayer)){
                return true;
            }
        }
        return false;
   }
   private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
   }
}
