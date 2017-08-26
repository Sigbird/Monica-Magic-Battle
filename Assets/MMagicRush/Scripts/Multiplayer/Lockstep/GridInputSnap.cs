using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInputSnap : MonoBehaviour {
    SpriteRenderer sprite;

    private static bool isMouseDown = false;
    private static Vector2 mouseUpGridPos;

    public static Vector2 GetGridInput() {
        return mouseUpGridPos;
    }

    void OnMouseDown() {
        isMouseDown = true;    
    }
    
    private void OnMouseOver() {        
        if (isMouseDown) {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        } else {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .2f);
        }        
    }    

    void OnMouseExit() {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }

    private void OnMouseUp() {
        isMouseDown = false;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        mouseUpGridPos = transform.localPosition;
    }
    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
