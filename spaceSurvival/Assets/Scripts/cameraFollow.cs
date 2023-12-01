using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool followTarget;
    [SerializeField] float smooth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (followTarget)
        {
            follow();
        }
        
    }
    void follow()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(target.position.x,target.position.y, Camera.main.transform.position.z), Time.time*target.gameObject.GetComponent<playerMovements_Spaceship>().maxSpeed);
       
    }
}
