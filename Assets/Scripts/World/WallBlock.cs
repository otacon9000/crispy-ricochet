using UnityEngine;

public class WallBlock : MonoBehaviour
{
    [SerializeField] private Material materialDefault;
    [SerializeField] private Material materialHard;

    private int      _maxHealth;
    private int      _currentHealth;
    private Renderer _renderer;

    public void Init(int health)
    {
        _maxHealth     = health;
        _currentHealth = health;
        _renderer      = GetComponent<Renderer>();

        bool isHard = health > 1;
        _renderer.material = isHard ? materialHard : materialDefault;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        UpdateColor();

        if (_currentHealth <= 0)
            gameObject.SetActive(false);
    }

    private void UpdateColor()
    {
        float t = 1f - ((float)_currentHealth / _maxHealth);
        Color c = Color.Lerp(
            new Color(0.55f, 0.55f, 1f),   // intatto
            new Color(1f,    0.13f, 0.13f), // quasi distrutto
            t
        );
        _renderer.material.color = c;
    }
}