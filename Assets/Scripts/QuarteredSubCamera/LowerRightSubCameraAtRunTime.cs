using UnityEngine;
using System.Collections;

public class LowerRightSubCameraAtRunTime : MonoBehaviour {
	private new Camera camera;

	// Use this for initialization
	void Start () {
		this.Initialize ();
	}

	private void Initialize(){
		this.camera = this.GetComponent<Camera> ();
		if (null == this.camera) {
			return;
		}
		this.camera.rect = new Rect (0.5f, 0, 1f, 0.5f);
	}
}
