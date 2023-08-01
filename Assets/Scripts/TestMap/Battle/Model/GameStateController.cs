using ProjectX.Battle.Model.Unit;

public class GameStateController
{
    /// <summary>
    /// ������
    /// </summary>
    public int Initialize(IUnitCreatable unitCreatable,IMapCreatable mapCreatable)
    {
        if(unitCreatable == null || mapCreatable == null)
        {
            return -1;
        }
        new UnitManager(unitCreatable.GetUnits());
        new MapData(mapCreatable.GetMap());

        return 0;
    }

    /// <summary>
    /// �I��
    /// </summary>
    public int UnInitialize()
    {
        return 0;
    }

}
