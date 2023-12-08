
using UniRx;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace ProjectX.Battle
{
    public class Grid
    {
        //グリッドのステータス[0:通行可能 1:通行不可]
        private ReactiveProperty<int> _status = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Status => _status;

        private ReactiveCollection<GridEffect> _gridEffects = new ReactiveCollection<GridEffect>();

        public IReactiveCollection<GridEffect> GridEffects => _gridEffects;

        //ステータスをセット
        public void SetStatus(int value)
        {
            _status.Value = value;
            //デバッグログ
            //Debug.Log($"{_status.Value} - HP:{_status.Value}");
        }

        public void AddGridEffect(GridEffect gridEffect)
        {
            _gridEffects.Add(gridEffect);
            // デバッグログ
            Debug.Log($"{gridEffect.ToString()} - HP:{gridEffect.ToString()}");
        }

    }

}


