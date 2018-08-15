using UnityEngine;

public class PlayerKeyTrigger : MonoBehaviour {
    
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			gameObject.GetComponent<Key>().ActivateKey();
		}
	}
}
