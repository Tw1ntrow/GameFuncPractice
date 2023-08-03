using ProjectX.Battle.Model.Unit;
using System.Linq;

/// <summary>
/// �����^�[��
/// ����ł��郆�j�b�g��T���A���j�b�g�𑀍삳����
/// ����ł��郆�j�b�g�����Ȃ��Ȃ�΃^�[���I��
/// </summary>
public class PlayerTurn : TurnBase, ITurnCycle
{
    public void StartTurn(UnitManager unitManager, MapData mapData)
    {
        // �^�[���J�n���ɍs���\�ȃ��j�b�g�����Ȃ���΃^�[���I��
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
        // �s���\�ȃ��j�b�g�����Ȃ���΃^�[���I��
        if (IsPlayerTurnEnd(unitManager))
        {
            EndTurn();
            return;
        }
        // �s���\�ȃ��j�b�g���擾
        var playerUnits = unitManager.GetUnitsByFaction(0);
        var unit = playerUnits.First(unit => unit.ActionPoint.Value >= 1);
        // �s����I������A�ЂƂ܂��͉�����
        // �{���͑I���ł���s����View�ɒʒm���AView����s����I��������
        unit.UnitActions[0].SetTarget(unit, unitManager, mapData);
        (unit.UnitActions[0] as IImmediateAction).Execute(unit, unitManager, mapData);
        
        // �܂��s���ł��郆�j�b�g������΍s��������
        // �{����View�ōs�����郆�j�b�g��I�ԁAView�����܂ł͍s���|�C���g���]���Ă��郆�j�b�g���擾����
        UnitAction(unitManager, mapData);
    }

    private bool IsPlayerTurnEnd(UnitManager unitManager)
    {
        var playerUnits = unitManager.GetUnitsByFaction(0);
        // �s���|�C���g��1�ȏ�̃��j�b�g�����Ȃ����true
        return !playerUnits.Any(unit => unit.ActionPoint.Value >= 1);
    }
    public void EndTurn()
    {
        NotifyActionCompleted();
    }
}