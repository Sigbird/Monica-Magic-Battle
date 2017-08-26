using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameGridGenerator : MonoBehaviour {
    public int VerticalSize = 16;
    public int HorizontalSize = 9;
    public float CellSize = 1f;
    public GameObject GridUnit;
	// Use this for initialization
	void Start () {
        
	}
	
    public void GenerateGrid() {
        var grid = new GameObject();
        grid.name = "GameGrid";

        for (int i = -HorizontalSize; i <= HorizontalSize; i++) {
            for (int j = -VerticalSize; j <= VerticalSize; j++) {
                var x = i * CellSize;
                var y = j * CellSize;

                var pos = new Vector2(x, y);
                var gridTile = Instantiate(GridUnit, pos, this.transform.rotation, grid.transform);
                gridTile.name = "Tile " + i + " " + j;                
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
