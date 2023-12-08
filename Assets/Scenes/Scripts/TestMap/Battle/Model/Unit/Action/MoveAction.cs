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
        // ������
        // �ړ����UI�Ō��߂鏈��������(View�ɒʒm����)
        dest = Actor.Position.Value + new UnityEngine.Vector2Int(1, 0);
        Debug.Log($"Target position for {Actor.Name.Value} set to {dest}");

        // �ړ��悪�ړ��ł��Ȃ��ꍇ�A�ړ��ł��Ȃ��I��UI�̕\���ɂ���
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