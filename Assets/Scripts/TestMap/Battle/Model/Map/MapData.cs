using ProjectX.Battle;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MapData
{
    private ReactiveDictionary<Vector2Int, ProjectX.Battle.Grid> _data { get; set; }
    public IReadOnlyReactiveDictionary<Vector2Int, ProjectX.Battle.Grid> Data => _data;

    private ReactiveProperty<Vector2Int> _size { get; set; }
    private IReadOnlyReactiveProperty<Vector2Int> Size => _size;

    public MapData(Dictionary<Vector2Int, ProjectX.Battle.Grid> map)
    {
        _data = new ReactiveDictionary<Vector2Int, ProjectX.Battle.Grid>();
        int maxX = int.MinValue;
        int maxY = int.MinValue;
        foreach (var grid in map)
        {
            _data.Add(grid.Key, grid.Value);

            // 現在のグリッドのXまたはY座標が最大値より大きい場合、最大値を更新
            if (grid.Key.x > maxX)
            {
                maxX = grid.Key.x;
            }
            if (grid.Key.y > maxY)
            {
                maxY = grid.Key.y;
            }
        }

        // マップのサイズを設定
        // グリッド座標は0から始まるため、1を加える
        _size = new ReactiveProperty<Vector2Int>(new Vector2Int(maxX + 1, maxY + 1));
    }

    public bool CanMove(Vector2Int position)
    {
        return _data.ContainsKey(position) && _data[position].Status.Value == 0;
    }

    /// <summary>
    /// 指定された位置にGridを設定します。
    /// </summary>
    public void SetGrid(Vector2Int position, ProjectX.Battle.Grid grid)
    {
        _data[position] = grid;
        Debug.Log($"Grid set at position {position}");
    }

    /// <summary>
    /// 指定された位置のGridを削除します。
    /// </summary>
    public void RemoveGrid(Vector2Int position)
    {
        _data.Remove(position);
        Debug.Log($"Grid removed from position {position}");
    }

    /// <summary>
    /// 指定された位置のGridを取得します。
    /// </summary>
    public ProjectX.Battle.Grid GetGrid(Vector2Int position)
    {
        Debug.Log($"Retrieving grid at position {position}");
        return _data[position];
    }

    /// <summary>
    /// マップ内の全てのGridを取得します。
    /// </summary>
    public List<ProjectX.Battle.Grid> GetAllGrids()
    {
        Debug.Log($"Retrieving all grids");
        return _data.Values.ToList();
    }

    /// <summary>
    /// 指定された位置から指定された範囲内の全てのGridを取得します。
    /// </summary>
    public List<ProjectX.Battle.Grid> GetRangeGrids(Vector2Int position, int range)
    {
        Debug.Log($"Retrieving grids within range {range} of position {position}");
        return _data.Where(pair => Vector2Int.Distance(position, pair.Key) <= range)
            .Select(pair => pair.Value)
            .ToList();
    }

    /// <summary>
    /// マップ内の二つのGridの位置を交換します。
    /// </summary>
    public void SwitchGrid(Vector2Int position1, Vector2Int position2)
    {
        var grid1 = _data[position1];
        var grid2 = _data[position2];
        _data[position1] = grid2;
        _data[position2] = grid1;
        Debug.Log($"Switched grids at positions {position1} and {position2}");
    }

    /// <summary>
    /// マップ内の二つのGridの位置を交換します。
    /// このオーバーロードは、Gridのオブジェクト参照によってGridを識別します。
    /// </summary>
    public void SwitchGrid(ProjectX.Battle.Grid grid1, ProjectX.Battle.Grid grid2)
    {
        var position1 = _data.FirstOrDefault(pair => pair.Value == grid1).Key;
        var position2 = _data.FirstOrDefault(pair => pair.Value == grid2).Key;
        Debug.Log($"Switching grid at position {position1} with grid at position {position2}");
        SwitchGrid(position1, position2);
    }
}