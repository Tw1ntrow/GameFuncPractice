
using UniRx;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace ProjectX.Battle
{
    public class Grid
    {
        //�O���b�h�̃X�e�[�^�X[0:�ʍs�\ 1:�ʍs�s��]
        private ReactiveProperty<int> _status = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Status => _status;

        private ReactiveCollection<GridEffect> _gridEffects = new ReactiveCollection<GridEffect>();

        public IReactiveCollection<GridEffect> GridEffects => _gridEffects;

        //�X�e�[�^�X���Z�b�g
        public void SetStatus(int value)
        {
            _status.Value = value;
            //�f�o�b�O���O
            //Debug.Log($"{_status.Value} - HP:{_status.Value}");
        }

        public void AddGridEffect(GridEffect gridEffect)
        {
            _gridEffects.Add(gridEffect);
            // �f�o�b�O���O
            Debug.Log($"{gridEffect.ToString()} - HP:{gridEffect.ToString()}");
        }

    }

}


