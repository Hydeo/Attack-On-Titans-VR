

public class HealthSystem {

    private int health;
	private int maxHealth;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }


    public void Damage(int damage)
    {
        this.health = this.health - damage < 0 ? 0 : this.health - damage;
    }


    public void Heal(int heal)
    {
        this.health = this.health + heal > this.maxHealth ? this.maxHealth : this.health + heal;
    }




}
