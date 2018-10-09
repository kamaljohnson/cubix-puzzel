using UnityEngine;

public class PlayerKeyTrigger : MonoBehaviour {
    
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			gameObject.GetComponent<Key>().ActivateKey();
		}
	}
}
