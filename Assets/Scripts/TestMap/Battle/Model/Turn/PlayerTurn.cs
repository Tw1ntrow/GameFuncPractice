using ProjectX.Battle.Model.Unit;
using System.Linq;

/// <summary>
/// 味方ターン
/// 操作できるユニットを探し、ユニットを操作させる
/// 操作できるユニットがいなくなればターン終了
/// </summary>
public class PlayerTurn : TurnBase, ITurnCycle
{
    public void StartTurn(UnitManager unitManager, MapData mapData)
    {
        // ターン開始時に行動可能なユニットがいなければターン終了
        if (!IsPlayerTurnEnd(unitManager))
        {
            UnitAction(unitManager, mapData);
        }
        else
        {
            EndTurn();
            return;
        }
    }

    private void UnitAction(UnitManager unitManager, MapData mapData)
    {
        // 行動可能なユニットがいなければターン終了
        if (IsPlayerTurnEnd(unitManager))
        {
            EndTurn();
            return;
        }
        // 行動可能なユニットを取得
        var playerUnits = unitManager.GetUnitsByFaction(0);
        var unit = playerUnits.First(unit => unit.ActionPoint.Value >= 1);
        // 行動を選択する、ひとまずは仮処理
        // 本来は選択できる行動をViewに通知し、Viewから行動を選択させる
        unit.UnitActions[0].SetTarget(unit, unitManager, mapData);
        (unit.UnitActions[0] as IImmediateAction).Execute(unit, unitManager, mapData);
        
        // まだ行動できるユニットが居れば行動させる
        // 本来はViewで行動するユニットを選ぶ、View実装までは行動ポイントが余っているユニットを取得する
        UnitAction(unitManager, mapData);
    }

    private bool IsPlayerTurnEnd(UnitManager unitManager)
    {
        var playerUnits = unitManager.GetUnitsByFaction(0);
        // 行動ポイントが1以上のユニットがいなければtrue
        return !playerUnits.Any(unit => unit.ActionPoint.Value >= 1);
    }
    public void EndTurn()
    {
        NotifyActionCompleted();
    }
}