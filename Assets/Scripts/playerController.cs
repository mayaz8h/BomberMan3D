using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    
    //state of player
    public bool dead = false;

    //GlobalStateManager, a script that is notified of all player deaths and determines which player won.?
    //public GlobalStateManager globalManager;
    
    //Camera Movement
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float MouseSensitivity = 3.5f;

    [SerializeField] bool lockCursor = true;

    [SerializeField] float walkspeed = 7.0f;

    CharacterController controller = null;

    [SerializeField][Range(0.0f, 0.5f)] float smoothTime = 0.3f; 
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f; 
    Vector2 curDir = Vector2.zero;
    Vector2 curVel = Vector2.zero;

    Vector2 curMouseDelta = Vector2.zero;
    Vector2 curMouseDeltaVelocity = Vector2.zero;

    float cameraPitch = 0f;

    //gravity
    [SerializeField] float gravity = -10f;
    
    //keep track of downwards speed
    float velocityY = 0.0f;

    public int numChests;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            UpdateMouseLook();
            UpdateMovement();
        }
    
    }

    //Mouse look controls
    void UpdateMouseLook() {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        curMouseDelta = Vector2.SmoothDamp(curMouseDelta, targetMouseDelta, ref curMouseDeltaVelocity, mouseSmoothTime);

        transform.Rotate(Vector3.up * curMouseDelta.x * MouseSensitivity);

        cameraPitch -= curMouseDelta.y * MouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

    }

    //movement controls
    void UpdateMovement() {

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        curDir = Vector2.SmoothDamp(curDir, targetDir, ref curVel, smoothTime);

        //Check if player is grounded, and if yes, make sure velocity = 0
        if (controller.isGrounded) {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * curDir.y + transform.right * curDir.x) * walkspeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        
        //fix player such that it cannot jump!
        if (transform.position.y > 1.33f) {
            transform.position = new Vector3(transform.position.x, 1.33f, transform.position.z);
        }


    }
    
    private void OnTriggerEnter(Collider other) {

      

        if (other.gameObject.layer == LayerMask.NameToLayer("Expl")) {
            Debug.Log("layer check");
            SceneManager.LoadScene(2);
        }

        //check if the layer of the gameObject Player just exited is Bomb
        else if (other.gameObject.layer == LayerMask.NameToLayer("Chests")) {
            
            //other aka bomb is no longer a trigger
            scoreManager.instance.updateScore();
            Destroy(other.gameObject);

        }

    }

    // private void PlayerDeath() {
    //     Debug.Log("death called");
    //     //PlayerDead
    //     dead = true;
    //     //Disable BombController
    //     GetComponent<BombController>().enabled = false;
        
    //     //Prevent anymore interactions
    //     gameObject.SetActive(false);
    //     //GameManager.Instance.UpdateGameState(GameState.Lose);

    // }


   
}
