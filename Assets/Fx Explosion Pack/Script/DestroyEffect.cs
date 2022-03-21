using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

	void Start()
	{
		Destroy(transform.gameObject, 2f);
	}
}
