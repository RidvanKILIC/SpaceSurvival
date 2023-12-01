using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playerInputController_Spaceship : MonoBehaviour
{
    Vector2 inputVector = Vector2.zero;
    playerMovements_Spaceship _playerMovement;
    Player _player;
    [SerializeField] FixedJoystick _joystick;
    [SerializeField] GameObject powerBtn;
    [SerializeField] float smoothTime;
    float power = 0;
    public bool isPowerUp = false;
    public bool canMove = true;
    bool warningActive = false;
    bool freezeDamageDelay = false;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = gameObject.GetComponent<playerMovements_Spaceship>();
        _player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        //inputVector.x = _joystick.Horizontal;
        ////inputVector.y = _joystick.Vertical;

#if PLATFORM_STANDALONE
        inputVector.x = Input.GetAxis("Horizontal");
        //inputVector.y = Input.GetAxis("Vertical");
#else
        inputVector.x = _joystick.Horizontal;
        //inputVector.y = _joystick.Vertical;
       
#endif

        checkBorders();
        if (isPowerUp && (_playerMovement.gameObject.transform.position.x >= _playerMovement.leftBound.position.x && _playerMovement.gameObject.transform.position.x <= _playerMovement.rightBound.position.x)
          && (_playerMovement.gameObject.transform.position.y <= _playerMovement.topBound.position.y && _playerMovement.gameObject.transform.position.y >= _playerMovement.bottomBound.position.y))
        {
            powerUp();
            //Debug.Log("Not Available");
        }
        else
        {
            powerDown();
            if ((_playerMovement.gameObject.transform.position.x <= _playerMovement.leftBound.position.x || _playerMovement.gameObject.transform.position.x >= _playerMovement.rightBound.position.x)
            || (_playerMovement.gameObject.transform.position.y >= _playerMovement.topBound.position.y || _playerMovement.gameObject.transform.position.y <= _playerMovement.bottomBound.position.y))
            {
                canMove = false;
                warningActive = false;
                //powerBtn.GetComponent<Button>().interactable = false;
                //powerBtn.GetComponent<EventTrigger>().enabled = false;
                _playerMovement.respawnPlayer();
                StartCoroutine(delayMove());
            }

        }

        if (canMove /*&& isPowerUp*/)
        {
            _playerMovement.setInputVector(inputVector);
        }

    }
    void checkBorders()
    {
        //Debug.Log("Player Pos x: " + (_playerMovement.gameObject.transform.position.x));
        //Debug.Log("LeftBound pos x:" + (_playerMovement.leftBound.position.x + 60f));
        //Debug.Log("RightBound pos x: " + (_playerMovement.rightBound.position.x - 60f));
        //Debug.Log("Player Pos y: " + (_playerMovement.gameObject.transform.position.y));
        //Debug.Log("Top Bound pos y: " + (_playerMovement.topBound.position.y - 60f));
        //Debug.Log("Bottom bound pos y: " + (_playerMovement.bottomBound.position.y + 60f));
        if ((_playerMovement.gameObject.transform.position.x <= _playerMovement.leftBound.position.x + 60f || _playerMovement.gameObject.transform.position.x >= _playerMovement.rightBound.position.x - 60f)
          || (_playerMovement.gameObject.transform.position.y >= _playerMovement.topBound.position.y - 60f || _playerMovement.gameObject.transform.position.y <= _playerMovement.bottomBound.position.y + 60f))
        {
            if (!freezeDamageDelay)
            {
                this.gameObject.GetComponent<IDamagable>().Damage(1);
                StartCoroutine(freezeDamageRoutine());
            }
            
            if (!warningActive)
            {
                warningActive = true;
                UI_Manager.UInstance.boderWarning();
            }

        }
        else if (((_playerMovement.gameObject.transform.position.x >= _playerMovement.leftBound.position.x + 60f && _playerMovement.gameObject.transform.position.x <= _playerMovement.rightBound.position.x - 60f)
          && (_playerMovement.gameObject.transform.position.y <= _playerMovement.topBound.position.y - 60f && _playerMovement.gameObject.transform.position.y >= _playerMovement.bottomBound.position.y + 60f)))
        {
            warningActive = false;
            UI_Manager.UInstance.resetFrost();
        }
    }
    public void onPowerButtonDown()
    {
        isPowerUp = true;
        GetComponentInChildren<Animator>().SetBool("powerUp", isPowerUp);
        //Debug.Log("isPowerUp:" + isPowerUp);
        //power += 0.1f;
        //Mathf.Clamp(power, 0f, 1f);
        //if (power <= 1f)
        //{
        //    inputVector.y = power;
        //}
        //else
        //{
        //    power = 1f;
        //    inputVector.y = power;
        //}
        //Debug.Log(power);

    }
    public void onPowerButtonUp()
    {
        isPowerUp = false;
        GetComponentInChildren<Animator>().SetBool("powerUp", isPowerUp);
        //Debug.Log("isPowerUp:" + isPowerUp);
        //power = 0;
        //inputVector.y = power;
    }
    void powerUp()
    {
        //power += Time.time;
        power = Mathf.Lerp(power, 1f, smoothTime);
        Mathf.Clamp(power, 0f, 1f);
        inputVector.y = power;
        //Debug.Log(power);
    }
    void powerDown()
    {
        power = 0f;
        inputVector.y = power;
        inputVector.x = power;
        
    }
    IEnumerator delayMove()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
        //powerBtn.GetComponent<Button>().interactable = true;
        //powerBtn.GetComponent<EventTrigger>().enabled = true;
        //Debug.Log("canMove: "+canMove);
    }
    IEnumerator freezeDamageRoutine()
    {
        freezeDamageDelay = true;
        yield return new WaitForSeconds(1);
        freezeDamageDelay = false;
    }
}
