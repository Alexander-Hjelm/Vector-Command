using UnityEngine;
using System.Collections;

public class PanController : MonoBehaviour {

	public GameObject panTarget;
	float panSpeed = 1f;
	float zoomSpeed = 1f;
	float cameraLerpSpeed = 5f;

	// Use this for initialization
	void Update () {
	
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, panTarget.transform.position, cameraLerpSpeed*Time.deltaTime);
		//Camera.main.transform.rotation = Quaternion.Slerp (Camera.main.transform.rotation, Quaternion.LookRotation(Vector3.down, player.transform.forward), camera_lerp_speed*Time.deltaTime);

		if (Input.GetMouseButton(1))
		{
			panTarget.transform.position += Input.GetAxis("Mouse X") * Camera.main.transform.right * panSpeed;
			panTarget.transform.position += Input.GetAxis("Mouse Y") * Camera.main.transform.up * panSpeed;

		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.GetComponent<Camera>().orthographicSize > 2)
		{
			Camera.main.GetComponent<Camera>().orthographicSize -= 0.5f;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.GetComponent<Camera>().orthographicSize < 10)
		{
			Camera.main.GetComponent<Camera>().orthographicSize += 0.5f;
		}
	}
}
