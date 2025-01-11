using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth = 100;
    [SerializeField] private bool Invincible = false;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
    public void Damage(float damage)
    {
        if (!Invincible)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        }

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }
    
    public void Death()
    {
        //After death event
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
