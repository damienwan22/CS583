using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour{
    [Header("Set in Inspector")]
    public static float bottomY = -20f;
    
    // Update is called once per frame
    void Update(){
        if(transform.position.y < bottomY){
            Destroy(this.gameObject);
            // Get a reference to the Apple Picker component of Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            //Ccall the public AppleDestroyed() method of apScript
            apScript.AppleDestroyed();
        }
        
    }
}
