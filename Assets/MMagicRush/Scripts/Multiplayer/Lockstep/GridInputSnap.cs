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

    public delegate void OnGridMouseUpAction(Vector2 mousePos);
    public static event OnGridMouseUpAction OnGridMouseUpEvent;

    void OnMouseDown() {
        isMouseDown = true;    
    }        

    void OnMouseOver() {        
        if (isMouseDown) {
            mouseUpGridPos = transform.localPosition;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        } else {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        }        
    }    

    void OnMouseExit() {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }

    private void OnMouseUp() {
        isMouseDown = false;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);        

        if (OnGridMouseUpEvent != null) OnGridMouseUpEvent(mouseUpGridPos);
    }
    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        isMouseDown = false;        
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
