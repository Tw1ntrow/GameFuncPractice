using System.Collections.Generic;
using UnityEngine;
using ProjectX.Battle;

public class TestMapCreatable : IMapCreatable
{
    // �S�Ēʍs�\��100�~100�̃}�b�v
    public Dictionary<Vector2Int, ProjectX.Battle.Grid> GetMap()
    {
        var map = new Dictionary<Vector2Int, ProjectX.Battle.Grid>();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var grid = new ProjectX.Battle.Grid();
                grid.SetStatus(0);

                map[new Vector2Int(i, j)] = grid;
            }
        }

        return map;
    }

}

// �������j�b�g�A�G���j�b�g�����ꂼ��5�̂���
public class TestUnitCreator : IUnitCreatable
{
    public List<Unit> GetUnits()
    {
        var units = new List<Unit>();

        for (int i = 0; i < 10; i++)
        {
            var unit = new Unit(i,100,10, new Vector2Int(i, i), $"Unit{i}", (i % 2),2);
            unit.AddAction(new MoveAction());
            units.Add(unit);
        }

        return units;
    }
}