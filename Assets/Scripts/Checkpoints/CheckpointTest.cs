using UnityEngine;

public class CheckpointTest : MonoBehaviour
{
    public void TestStartCheckpoint(int number)
    {
        Debug.Log("Checkpoint started " + number.ToString());
    }
    public void TestLoadCheckpoint(int number)
    {
        Debug.Log("Checkpoint loaded " + number.ToString());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<Health>().Death();
        }
    }
}
