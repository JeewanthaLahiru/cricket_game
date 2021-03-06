using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    private Quaternion CamRotation;
    public float CamSpeed = 1f;
    public Transform player;
    public float RotationSpeed = 5f;
    private float forwardInput, RightInput;
    private float mouseX, mouseY;

    void Start()
    {
        CamRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {

        mouseX += Input.GetAxis("Mouse X") * CamSpeed ;
        mouseY -= Input.GetAxis("Mouse Y") * CamSpeed ;

        mouseY = Mathf.Clamp(mouseY, -30f, 80f);
        transform.rotation = Quaternion.Euler(mouseY , mouseX, 0);
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0, mouseX, 0), Time.deltaTime * RotationSpeed);
            //player.transform.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        
    }

    
}
