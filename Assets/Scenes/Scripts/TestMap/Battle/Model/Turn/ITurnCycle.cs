using ProjectX.Battle.Model.Unit;

public interface ITurnCycle
{
    public void StartTurn(UnitManager unitManager,MapData mapData);
    public void EndTurn();
}
