using UnityEngine;

public class NotebookScript : MonoBehaviour
{
    public float openingDistance;
    public GameControllerScript gc;
    public GameObject learningGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Vector3.Distance(other.transform.position, transform.position) < openingDistance)
        {
            gameObject.SetActive(false);
            gc.CollectNotebook();
            learningGame.SetActive(true);
        }
    }
}
