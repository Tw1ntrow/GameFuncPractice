using ProjectX.Battle;
using ProjectX.Battle.Model.Unit;
/// <summary>
/// ‘¦”­“®‚·‚és“®
/// </summary>
public interface IImmediateAction
{
    public void Execute(Unit Actor, UnitManager unitManager, MapData mapData);
}
