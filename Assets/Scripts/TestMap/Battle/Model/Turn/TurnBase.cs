using System;
using UniRx;

public abstract class TurnBase
{
    protected Subject<TurnBase> onTurnEnd = new Subject<TurnBase>();
    public IObservable<TurnBase> OnTurnEnd => onTurnEnd;

    protected void NotifyActionCompleted()
    {
        onTurnEnd.OnNext(this);
    }
}
