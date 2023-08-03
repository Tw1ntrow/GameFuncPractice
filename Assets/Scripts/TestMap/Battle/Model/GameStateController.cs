using ProjectX.Battle.Model.Unit;
using UniRx;

public class GameStateController
{
    /// <summary>
    /// ‰Šú‰»
    /// </summary>
    public int Initialize(IUnitCreatable unitCreatable,IMapCreatable mapCreatable)
    {
        if(unitCreatable == null || mapCreatable == null)
        {
            return -1;
        }
        var unitManager = new UnitManager(unitCreatable.GetUnits());
        var mapData = new MapData(mapCreatable.GetMap());
        var turn = new PlayerTurn();
        turn.StartTurn(unitManager, mapData);
        turn.OnTurnEnd.Subscribe(_ => UnInitialize());
        return 0;
    }

    /// <summary>
    /// I—¹
    /// </summary>
    public int UnInitialize()
    {
        return 0;
    }

}
