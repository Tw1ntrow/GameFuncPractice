using ProjectX.Battle;
using ProjectX.Battle.Model.Unit;
/// <summary>
/// ������������s��
/// </summary>
public interface IImmediateAction
{
    public void Execute(Unit Actor, UnitManager unitManager, MapData mapData);
}
