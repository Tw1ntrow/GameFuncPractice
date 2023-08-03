using ProjectX.Battle;
using ProjectX.Battle.Model.Unit;
using UnityEngine;

public class MoveAction : UnitAction, IImmediateAction
{
    private Vector2Int dest;
    public override bool CanAction(Unit Actor)
    {
        return Actor.ActionPoint.Value > 0;
    }

    public void Execute(Unit Actor, UnitManager unitManager, MapData mapData)
    {
        Actor.SetPosition(dest);
        Actor.SetActionPoint(Actor.ActionPoint.Value - 1);
        Debug.Log($"{Actor.Name.Value} moved to {dest}");
    }

    public override void SetTarget(Unit Actor, UnitManager unitManager, MapData mapData)
    {
        // 仮処理
        // 移動先をUIで決める処理を入れる(Viewに通知する)
        dest = Actor.Position.Value + new UnityEngine.Vector2Int(1, 0);
        Debug.Log($"Target position for {Actor.Name.Value} set to {dest}");

        // 移動先が移動できない場合、移動できない的なUIの表現にする
        if (mapData.CanMove(dest))
        {
            Debug.Log($"Position {dest} is moveable");
        }
        else
        {
            Debug.Log($"Position {dest} is not moveable");
        }
    }
}