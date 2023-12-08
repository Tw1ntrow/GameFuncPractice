using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField]
    private Vector2 gridSize;

    public Vector2 GridSize { get => gridSize; set => gridSize = value; }
    public static float TileSize = 6f;


    // �O���b�h�̍��W�����[���h���W�ɕϊ����郁�\�b�h
    public static Vector3 GetWorldPositionFromGrid(Vector2Int grid)
    {
        return new Vector3(grid.x * MapView.TileSize, 0, grid.y * MapView.TileSize);
    }

    public static Vector3 GetWorldPositionCenterFromGrid(Vector2Int grid)
    {
        return new Vector3(grid.x * MapView.TileSize + (MapView.TileSize / 2f), 0, grid.y * MapView.TileSize + (MapView.TileSize / 2f));
    }

    public static Vector2Int GetGridFromWorldPosition(Vector3 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPos.x / MapView.TileSize), Mathf.FloorToInt(worldPos.z / MapView.TileSize));
    }


    public void Create()
    {

    }

    
}
