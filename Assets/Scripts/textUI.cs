using UnityEngine;
using UnityEngine.UI;

public class textUI : MonoBehaviour
{
    public Text stolenFoodText;
    public Text playerHealthText;
    public Text gameTimeText;
    // public Text dogDespawnTimeText;
    public Text score;
    public Text ammo;

    public Gamemode gamemode;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        // Update stolen food text
        stolenFoodText.text = "Stolen Food: " + gamemode.stolenFood;

        // Update player health text
        playerHealthText.text = "Player Health: " + player.getPlayerHealth();

        // Update game time text
        gameTimeText.text = "Game Time: " + Mathf.Floor(Time.time);

        //update score
        
        score.text = "Total Score: " + player.score;

        //update ammo
        ammo.text =  player.getAmmo() + " / 1 Ammo" ;

        // Update dog despawn time text (assuming you have a despawn timer for dogs)
        // Replace 'despawnTime' with your actual dog despawn timer value
        //dogDespawnTimeText.text = "Dog Despawn Time: " + (7f - Time.time); // Assuming despawnTime is a float representing the despawn time
    }
}
