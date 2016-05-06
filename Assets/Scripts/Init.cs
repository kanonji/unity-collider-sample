using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Initialize());
	}

	private static Scene LoadScene(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
		return SceneManager.GetSceneAt (SceneManager.sceneCount - 1); //LoadSceneに失敗すると1個前のシーンを取得してしまう。
	}

	private IEnumerator WaitForSceneLoad(Scene scene){
		while (false == scene.isLoaded) {
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator GetObjectsFromScene(Scene scene, UnityAction<GameObject[]> callback){
		yield return StartCoroutine(WaitForSceneLoad(scene));
		callback(scene.GetRootGameObjects ());
	}

	private IEnumerator InitializeStaticCollider(){
		GameObject[] objs = new GameObject[0];

		var scene = LoadScene ("Floor");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("Barricade");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("StaticCollider");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("QuarteredSubCamera");
		yield return StartCoroutine (GetObjectsFromScene (scene, r =>
		{
			GameObject cameraObj = r.Where(obj => null != obj.GetComponent<Camera>()).SingleOrDefault();
			if (null != cameraObj)
				cameraObj.AddComponent<UpperRightSubCameraAtRunTime>();
			objs = objs.Concat(r).ToArray();
		}));

		foreach (var obj in objs) {
			obj.transform.position += new Vector3 (50, 0, 0);
		}
	}

	private IEnumerator InitializeRigidbodyCollider(){
		GameObject[] objs = new GameObject[0];

		var scene = LoadScene ("Floor");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("Barricade");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("RigidbodyCollider");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("QuarteredSubCamera");
		yield return StartCoroutine (GetObjectsFromScene (scene, r =>
		{
			GameObject cameraObj = r.Where(obj => null != obj.GetComponent<Camera>()).SingleOrDefault();
			if (null != cameraObj)
				cameraObj.AddComponent<UpperLeftSubCameraAtRunTime>();
			objs = objs.Concat(r).ToArray();
		}));

		foreach (var obj in objs) {
			obj.transform.position += new Vector3 (100, 0, 0);
		}
	}

	private IEnumerator InitializeKitematicRigidbodyCollider(){
		GameObject[] objs = new GameObject[0];

		var scene = LoadScene ("Floor");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("Barricade");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("KinematicRigidbodyCollider");
		yield return StartCoroutine (GetObjectsFromScene (scene, r => objs = objs.Concat(r).ToArray()));

		scene = LoadScene ("QuarteredSubCamera");
		yield return StartCoroutine (GetObjectsFromScene (scene, r =>
		{
			GameObject cameraObj = r.Where(obj => null != obj.GetComponent<Camera>()).SingleOrDefault();
			if (null != cameraObj)
				cameraObj.AddComponent<LowerLeftSubCameraAtRunTime>();
			objs = objs.Concat(r).ToArray();
		}));

		foreach (var obj in objs) {
			obj.transform.position += new Vector3 (150, 0, 0);
		}
	}

	private IEnumerator Initialize(){
		yield return StartCoroutine (InitializeStaticCollider ());
		yield return StartCoroutine (InitializeRigidbodyCollider ());
		yield return StartCoroutine (InitializeKitematicRigidbodyCollider ());
	}
}
