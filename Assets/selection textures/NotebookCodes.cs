using UnityEngine;

public class NotebookCodes : MonoBehaviour
{
    public float openingDistance;
    public GameControllerScript gc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Vector3.Distance(other.transform.position, transform.position) < openingDistance)
        {
            gameObject.SetActive(false);
            gc.CollectNotebook();
        }
    }
}
