using UnityEngine;
using System.Collections;

public class MoveAhead : MonoBehaviour {
	private float generatedTime;

	// Use this for initialization
	void Start () {
		generatedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > generatedTime + 3f && Time.time < generatedTime + 9f) {
			this.transform.Translate (transform.right * -1 * Time.deltaTime * 3f);
		}
	}
}
