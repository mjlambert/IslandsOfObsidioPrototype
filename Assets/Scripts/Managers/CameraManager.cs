using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public Camera mainCamera;
    public float cameraSpeed;
    public float zoomSpeed;

    private float xPos;
    private float yPos;
    private float zPos;


    // Use this for initialization
    void Start ()
    {
        xPos = mainCamera.transform.position.x;
        yPos = mainCamera.transform.position.y;
        zPos = mainCamera.transform.position.z;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Movement and zoom

        float movementSpeed = cameraSpeed * (yPos / 10);

        float translationH = (Input.GetAxis("Horizontal") * movementSpeed) * Time.deltaTime;
        float translationV = (Input.GetAxis("Vertical") * movementSpeed) * Time.deltaTime;

        float scroll = (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) * Time.deltaTime;
        float cameraAngle = 45 * Mathf.Deg2Rad;
        float yDiff = (Mathf.Cos(cameraAngle) * scroll) * -1;
        float zDiff = Mathf.Sin(cameraAngle) * scroll;

        xPos += translationH;
        yPos += yDiff;
        zPos += translationV + zDiff;

        mainCamera.transform.position = new Vector3(xPos, yPos, zPos);

    }
}
