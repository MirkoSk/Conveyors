using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridController : MonoBehaviour
{
    [SerializeField] Vector2 gridSize = Vector2.zero;
    [SerializeField] GameObject hexagonPrefab = null;

    Grid grid;
    


    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Start()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                GameObject newCell = Instantiate(hexagonPrefab, grid.CellToWorld(new Vector3Int(x, y, 0)), Quaternion.identity, transform);
                newCell.name = "(" + x + "," + y + ")";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
