using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineNut : MonoBehaviour
{
    [Header("Set in Inspector")]
    public static float bottomY = -20f;
    public float duration = 5.0f;
    public float sizeMultiplier = 1.5f;

    // Update is called once per frame
    void Update(){
        if(transform.position.y < bottomY){
            Destroy(this.gameObject);
            // Get a reference to the Apple Picker component of Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            //Ccall the public AppleDestroyed() method of apScript
            apScript.PineNutDestroyed();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player collects the power-up
        {
            // Activate the power-up and trigger its effect
            Basket basket = other.GetComponent<Basket>();
            if (basket != null)
            {
                basket.ActivatePineNut(duration, sizeMultiplier);
            }

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }
}