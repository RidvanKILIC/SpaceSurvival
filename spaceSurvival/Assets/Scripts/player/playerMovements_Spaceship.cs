using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovements_Spaceship : MonoBehaviour
{
    public Rigidbody2D rb;
    public float accelarationFactor = 30.0f
                , turnFactor = 3.0f
                , rotationAngle = 0
                , driftFactor = 0.92f
                , maxSpeed = 20f;

    float accelarionInput = 0
         , steeringInput = 0
         , velocityVsUp = 0;


    [SerializeField]public Transform leftBound;
    [SerializeField]public Transform rightBound;
    [SerializeField]public Transform topBound;
    [SerializeField]public Transform bottomBound;
    [SerializeField] playerPos _playerPos;
    [SerializeField]playerInputController_Spaceship _input;
    GameObject _player;
   
    enum playerPos
    {
        Top,
        Bottom,
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = this.gameObject;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //freeMovement();
    //}
    //public void freeMovement()
    //{
    //    moveX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * DirectionalSpeed;
    //    moveY = Input.GetAxisRaw("Vertical") * Time.deltaTime * DirectionalSpeed;
    //    rb.velocity = new Vector3(moveX, moveY,0);
    //}
    private void FixedUpdate()
    {
        if (_input.canMove && _input.isPowerUp )
        {
            ApplySteering();
            ApplyEngineForce();
            KillOrtogonalVelocity();
        }

    }
    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocityVsUp > maxSpeed && accelarionInput > 0)
        {
            return;
        }
        else if (velocityVsUp > -maxSpeed * 0.5f && accelarionInput < 0)
        {
            return;
        }
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelarionInput > 0)
        {
            return;
        }
        if (accelarionInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }
        Vector2 engineForceVector = transform.up * accelarionInput * accelarationFactor;
        
            rb.AddForce(engineForceVector, ForceMode2D.Impulse);


    }
    public void respawnPlayer()
    {
        UI_Manager.UInstance.freeze();
        //Debug.Log("called");
        //_player.gameObject.SetActive(false);
        if (_player.transform.position.x <= leftBound.position.x)
        {
            _playerPos = playerPos.Left;
        }
        else if (_player.transform.position.x >= rightBound.position.x)
        {
            _playerPos = playerPos.Right;
        }
        else if (_player.transform.position.y <= bottomBound.position.y)
        {
            _playerPos = playerPos.Bottom;
        }
        else if (_player.transform.position.y >= topBound.position.y)
        {
            _playerPos = playerPos.Top;
        }
        resetRotation();
        resetVelocity();
        ApplySteering();
        ApplyEngineForce();
        KillOrtogonalVelocity();
        spawnPlayer();
    }
    void spawnPlayer()
    {
        ///Adjust player position
        if (_playerPos == playerPos.Bottom)
        {
            _player.transform.position = new Vector3(_player.transform.position.x, (bottomBound.transform.position.y + 80f), _player.transform.position.z);
        }
        else if (_playerPos == playerPos.Top)
        {
            _player.transform.position = new Vector3(_player.transform.position.x, (topBound.transform.position.y - 80f), _player.transform.position.z);
        }
        else if (_playerPos == playerPos.Left)
        {
            _player.transform.position = new Vector3(_player.transform.position.x, (leftBound.transform.position.y + 80f), _player.transform.position.z);
        }
        else if (_playerPos == playerPos.Right)
        {
            _player.transform.position = new Vector3(_player.transform.position.x, (rightBound.transform.position.y - 80f), _player.transform.position.z);
        }
        //_player.gameObject.SetActive(true);
        rb.WakeUp();
        _player.GetComponentInChildren<Animator>().SetTrigger("respawn");
        UI_Manager.UInstance.resetFrost();
    }
    void resetRotation()
    {
        rb.MoveRotation(0f);
    }
    void resetVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (rb.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        rb.MoveRotation(rotationAngle);
    }
    public void setInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelarionInput = inputVector.y;
    }
    void KillOrtogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rigidVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVelocity + rigidVelocity * driftFactor;
    }

}
