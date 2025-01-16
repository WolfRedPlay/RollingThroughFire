using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Transform for saving player position. Yf it is not assigned, the transform of object will be used.")]
    [SerializeField] Transform _saveTransform;

    Stage _stage;

    public Transform SaveTransform => _saveTransform;
    public void SetStage(Stage stage) {  _stage = stage; }

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        if (_saveTransform == null) _saveTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stage.StartStage();
            _stage.LoadStage();
            SetCheckpointActive(false);
        }
    }

    public void SetCheckpointActive(bool active)
    {
        gameObject.SetActive(active);
        if (_saveTransform == null) return;
        if (_saveTransform.gameObject.activeInHierarchy != active) _saveTransform.gameObject.SetActive(active);
    }
}
