using ProjectX.Battle;
using ProjectX.Battle.Model.Unit;



public abstract class UnitAction
{
    public abstract bool CanAction(Unit Actor);
    public abstract void SetTarget(Unit Actor, UnitManager unitManager, MapData mapData);

    //protected Subject<Unit> onSetTargetedCompleted = new Subject<Unit>();
    //public IObservable<Unit> OnSetTargetedCompleted => onSetTargetedCompleted;

    //protected void NotifyActionCompleted(Unit Actor)
    //{
    //    onSetTargetedCompleted.OnNext(Actor);
    //}
}
