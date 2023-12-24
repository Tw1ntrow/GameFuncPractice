using UniRx;
using UnityEngine;

namespace ProjectX.Battle
{

    public class Unit
    {
        // ID
        private ReactiveProperty<int> _id = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Id => _id;
        // HP
        private ReactiveProperty<int> _hp = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> HP => _hp;

        // �U����
        private ReactiveProperty<int> _attack = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Attack => _attack;

        // ���j�b�g�̈ʒu
        private ReactiveProperty<Vector2Int> _position = new ReactiveProperty<Vector2Int>();
        public IReadOnlyReactiveProperty<Vector2Int> Position => _position;

        // unit�̖��O
        private ReactiveProperty<string> _name = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> Name => _name;

        // �w�c[0:���� 1:�G 2:������]
        private ReactiveProperty<int> _faction = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Faction => _faction;

        private ReactiveProperty<int> _actionPoint = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> ActionPoint => _actionPoint;

        // ���j�b�g�̍s��
        // ���j�b�g���o����s��(���̃��j�b�g��p�����[�^�[�ɉe�����y�ڂ�����)�͑S�čs�����ĕ\������A�����Ɋi�[�����
        private ReactiveCollection<UnitAction> unitActions = new ReactiveCollection<UnitAction>();
        public IReadOnlyReactiveCollection<UnitAction> UnitActions => unitActions;

        public Unit(int id, int hp, int attack, Vector2Int position, string name, int faction, int actionPoint)
        {
            _id.Value = id;
            _hp.Value = hp;
            _attack.Value = attack;
            _position.Value = position;
            _name.Value = name;
            _faction.Value = faction;
            _actionPoint.Value = actionPoint;
        }

        public void AddAction(UnitAction action)
        {
            unitActions.Add(action);
        }

        public void RemoveAction(UnitAction action)
        {
            unitActions.Remove(action);
        }

        public void SetActionPoint(int value)
        {
            _actionPoint.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - ActionPoint:{_actionPoint.Value}");
        }

        public void SetPosition(Vector2Int value)
        {
            _position.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - Position:{_position.Value}");
        }

        public void SetId(int value)
        {
            _id.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - ID:{_id.Value}");
        }

        // HP�ƍU���͂�ݒ肷�邽�߂̃��\�b�h
        public void SetHP(int value)
        {
            _hp.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - HP:{_hp.Value}");
        }

        public void SetAttack(int value)
        {
            _attack.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - Attack:{_attack.Value}");

        }

        //���O��ݒ�
        public void SetName(string value)
        {
            _name.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - Name:{_name.Value}");
        }

        //�w�c��ݒ�
        public void SetFaction(int value)
        {
            _faction.Value = value;
            //�f�o�b�O���O
            Debug.Log($"{_name.Value} - Faction:{_faction.Value}");
        }

        public void TakeDamage(int damage)
        {
            _hp.Value -= damage;

            // HP��0�ȉ��ɂȂ�����A0�ɃN���b�v����i�̗͂��}�C�i�X�ɂȂ�̂�h���j
            _hp.Value = Mathf.Max(_hp.Value, 0);

            Debug.Log($"{_name.Value} - Took {damage} damage, remaining HP: {_hp.Value}");
        }

    }
}