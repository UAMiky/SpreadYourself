using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class MoverCamara : MonoBehaviour {

	// Use this for initialization
	Vector2 touchDeltaPosition;
	public float sensibility = 0.5f;


	public float time=5;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (UnityEngine.XR.XRSettings.enabled)
			return;

		if (Input.touchCount == 1) {
			
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Moved) {
				
				touchDeltaPosition = Input.GetTouch (0).deltaPosition * sensibility;

				gameObject.transform.eulerAngles += new Vector3 (touchDeltaPosition.y, -touchDeltaPosition.x, 0);
				time = 10f;
			}
		}

		else {
			time -= Time.deltaTime;

			if(time<=0)
			gameObject.transform.eulerAngles += Vector3.up * sensibility;
		}



	}

//	private GUIStyle myStyle = new GUIStyle ();
//	void OnGUI(){
//
//		myStyle.fontSize = 300;
//		GUI.Box (new Rect (10f, 10f, 500f,300f), " " + gameObject.GetComponent<Camera> ().fieldOfView,myStyle);
//	}
}
