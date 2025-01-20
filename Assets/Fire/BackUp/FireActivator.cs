using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireActivator : MonoBehaviour
{
    private SphereCollider m_Collider;
    [SerializeField] private string FireTag = "Fire";
    [SerializeField] private float SpeedOfGrowing = 1f;
    [SerializeField] private float MaxRadius = 10f;
    [SerializeField] GameObject Father;
    bool m_Enabled = false;

    public void ActiveFire()
    {
        m_Enabled = true;
    }

    public void KillFather()
    {
        if (Father != null) 
        {
            Destroy(Father);
        }
    }

    private void Awake()
    {
        m_Collider = GetComponent<SphereCollider>();
        m_Collider.isTrigger = true;
    }

    private void Update()
    {
        if (m_Enabled)
        {
            if (m_Collider.radius < MaxRadius)
            {
                m_Collider.radius += SpeedOfGrowing * Time.deltaTime;
                m_Collider.radius = Mathf.Min(m_Collider.radius, MaxRadius);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trtigger");
        if (other.transform.CompareTag(FireTag))
        {
            Debug.Log("Found fire");
            foreach (Transform child in other.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}