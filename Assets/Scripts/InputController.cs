using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputController : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap = null;
    [SerializeField] Tilemap roadTilemap = null;
    [SerializeField] TileData buildableTiles = null;
    [SerializeField] TileData roadTiles = null;

    [Space]
    [SerializeField] TileBase defaultRoadTile = null;

    Vector3 debugPosition;
    TileData.Connections neighbours = new TileData.Connections();



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(debugPosition, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z -= Camera.main.transform.position.z;
            Vector3Int gridPosition = groundTilemap.WorldToCell(position);
            TileBase tile = groundTilemap.GetTile(gridPosition);

            if (tile != null && buildableTiles.tiles.Contains(tile))
            {
                Debug.Log("Building road at (" + gridPosition.x + "," + gridPosition.y + ")!");
                roadTilemap.SetTile(gridPosition, GetRoadTile(gridPosition));
                UpdateNeighbours(gridPosition);
            }

            debugPosition = groundTilemap.GetCellCenterWorld(gridPosition);
        }
    }

    TileBase GetRoadTile(Vector3Int gridPosition)
    {
        TileBase returnTile = defaultRoadTile;

        CreateNeighboursArray(gridPosition);

        foreach (TileData.RoadTileData roadTile in roadTiles.roadTiles)
        {
            if (roadTile.tile != null 
                && roadTile.connections.TopLeft == neighbours.TopLeft && roadTile.connections.TopRight == neighbours.TopRight
                && roadTile.connections.Left == neighbours.Left && roadTile.connections.Right == neighbours.Right
                && roadTile.connections.BottomLeft == neighbours.BottomLeft && roadTile.connections.BottomRight == neighbours.BottomRight)
            {
                returnTile = roadTile.tile;
                break;
            }
        }

        return returnTile;
    }

    void CreateNeighboursArray(Vector3Int gridPosition)
    {
        // y even -> tile indented to the left
        if (gridPosition.y % 2 == 0)
        {
            // upper left neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y + 1, 0)) != null) neighbours.TopLeft = true;
            else neighbours.TopLeft = false;
            // upper right neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0)) != null) neighbours.TopRight = true;
            else neighbours.TopRight = false;
            // right neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0)) != null) neighbours.Right = true;
            else neighbours.Right = false;
            // bottom right neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0)) != null) neighbours.BottomRight = true;
            else neighbours.BottomRight = false;
            // bottom left neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y - 1, 0)) != null) neighbours.BottomLeft = true;
            else neighbours.BottomLeft = false;
            // left neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0)) != null) neighbours.Left = true;
            else neighbours.Left = false;
        }
        // y uneven -> tile indented to the right
        else
        {
            // upper left neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0)) != null) neighbours.TopLeft = true;
            else neighbours.TopLeft = false;
            // upper right neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, 0)) != null) neighbours.TopRight = true;
            else neighbours.TopRight = false;
            // right neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0)) != null) neighbours.Right = true;
            else neighbours.Right = false;
            // bottom right neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y - 1, 0)) != null) neighbours.BottomRight = true;
            else neighbours.BottomRight = false;
            // bottom left neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0)) != null) neighbours.BottomLeft = true;
            else neighbours.BottomLeft = false;
            // left neighbour
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0)) != null) neighbours.Left = true;
            else neighbours.Left = false;
        }
    }

    void UpdateNeighbours(Vector3Int gridPosition)
    {
        if (gridPosition.y % 2 == 0)
        {
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y + 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y + 1, 0), GetRoadTile(new Vector3Int(gridPosition.x - 1, gridPosition.y + 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0), GetRoadTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0), GetRoadTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0), GetRoadTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y - 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y - 1, 0), GetRoadTile(new Vector3Int(gridPosition.x - 1, gridPosition.y - 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0), GetRoadTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0)));
        }
        else
        {
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0), GetRoadTile(new Vector3Int(gridPosition.x, gridPosition.y + 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, 0), GetRoadTile(new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0), GetRoadTile(new Vector3Int(gridPosition.x + 1, gridPosition.y, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y - 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x + 1, gridPosition.y - 1, 0), GetRoadTile(new Vector3Int(gridPosition.x + 1, gridPosition.y - 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0), GetRoadTile(new Vector3Int(gridPosition.x, gridPosition.y - 1, 0)));
            if (roadTilemap.GetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0)) != null) roadTilemap.SetTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0), GetRoadTile(new Vector3Int(gridPosition.x - 1, gridPosition.y, 0)));
        }
    }
}
