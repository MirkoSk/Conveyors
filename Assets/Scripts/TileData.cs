using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObjects")]
public class TileData : ScriptableObject
{
    [System.Serializable]
    public class Connections
    {
        public bool TopLeft,
        TopRight,
        Right,
        BottomRight,
        BottomLeft,
        Left;
    }

    [System.Serializable]
    public class RoadTileData
    {
        public TileBase tile;
        public Connections connections;
    }

    public List<TileBase> tiles = new List<TileBase>();
    public bool buildable = true;
    public List<RoadTileData> roadTiles = new List<RoadTileData>();
}
