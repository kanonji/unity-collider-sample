using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ForceMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera[] cameras = FindComponentsOfType<Camera> ();
		foreach (Camera camera in cameras) {
			camera.enabled = false;
		}
		this.GetComponent<Camera> ().enabled = true;
	}

	private static T[] FindComponentsOfType <T>() where T : Component {
		List<T> result = new List<T> ();
		Object[] objects = Object.FindObjectsOfType (typeof(GameObject));
		foreach(GameObject obj in objects){
			T component = obj.GetComponent<T> ();
			//if ("null" != component.ToString()){
			if (null != component) {
				result.Add (component);
			}
		}
		return result.ToArray ();
	}
}
