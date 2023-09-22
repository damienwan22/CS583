using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //enables the use of uGUI features
using TMPro;

public class Basket : MonoBehaviour{
    [Header("Set Dynamically")]
    public TextMeshProUGUI scoreGT;

    // Start is called before the first frame update
    void Start(){
        // Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter"); 
        // Get the Text Component of that GameObject
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>(); 
        // Set the starting number of points to 0
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update(){
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;
        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D ); 
        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;

        if (isPineNutActive){
            PineNutDuration -= Time.deltaTime;
            if (PineNutDuration <=0f){
                // Power-up duration has expired, deactivates the pwer-up
                isPineNutActive = false;
                // Resets the basket size to its original size
                transform.localScale = new Vector3(originalBasketSize.x, originalBasketSize.y, originalBasketSize.z);
            }
        }
    }

    void OnCollisionEnter( Collision coll ){ 
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if ( collidedWith.tag == "Apple" ) {
            Destroy( collidedWith );
            // Parse the text of the scoreGT into an int
            int score = int.Parse( scoreGT.text );
            // Add points for catching the apple
            score += 100;
            // Convert the score back to a string and display it
            scoreGT.text = score.ToString();
            // Track the high score
            if (score > HighScore.score) {
                HighScore.score = score;
                }
        }
        else if (collidedWith.tag == "PineNut") {
            // Handle the power-up activation
            ActivatePineNut(5.0f, 2f); // Example values, adjust as needed
            // Destroy the power-up object
            Destroy(collidedWith);
        }
    }

    private bool isPineNutActive = false;
    private float PineNutDuration = 0f; // Adjust the duration as needed
    private Vector3 originalBasketSize; // Store the original basket size

    // Define a method to activate the basket size power-up
    public void ActivatePineNut(float duration, float sizeMultiplier)
    {
        if (!isPineNutActive)
        {
            isPineNutActive = true;
            PineNutDuration = duration;
            originalBasketSize = transform.localScale;
            // Adjust the basket size here
            Vector3 newSize = new Vector3(originalBasketSize.x * sizeMultiplier, originalBasketSize.y, originalBasketSize.z);
            transform.localScale = newSize;
            StartCoroutine(DeactivatePineNut());
            // You can also change the visual appearance of the basket if needed
            }
            }
    
    private IEnumerator DeactivatePineNut(){
        yield return new WaitForSeconds(PineNutDuration);
        if (isPineNutActive){
            // Deactivates the power-up after the duration expires
            isPineNutActive = false;
            // Reset the basket size to its initial size
           
            transform.localScale = originalBasketSize;
        }
    }
            
    // Add the method to reset basket size (as previously described)
    private void ResetBasketSize()
    {
        isPineNutActive = false;
        transform.localScale = new Vector3(originalBasketSize.x, originalBasketSize.y, originalBasketSize.z);
        }
}
