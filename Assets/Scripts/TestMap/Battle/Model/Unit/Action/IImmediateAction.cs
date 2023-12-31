using ProjectX.Battle;
using ProjectX.Battle.Model.Unit;
/// <summary>
/// 即時発動する行動
/// </summary>
public interface IImmediateAction
{
    public void Execute(Unit Actor, UnitManager unitManager, MapData mapData);
}
