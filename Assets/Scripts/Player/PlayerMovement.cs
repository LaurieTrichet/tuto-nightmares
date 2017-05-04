using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    public float speed = 6f;

    private Vector3 movement;
    private Animator animator;
    private Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake() {
        floorMask = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator> ();
        playerRigidbody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate() {
        float horizontal = Input.GetAxisRaw ("Horizontal");
        float vertical = Input.GetAxisRaw ("Vertical");

        Move(horizontal, vertical);
        Turning();
        Animating(horizontal, vertical);
    }

    void Move (float horizontal, float vertical){
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning () {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit; 

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating (float horizontal, float vertical){
        bool isWalking = horizontal != 0f || vertical != 0f;
        animator.SetBool("IsWalking", isWalking);
    }
}
