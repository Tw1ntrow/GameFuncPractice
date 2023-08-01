using UniRx;
using UnityEngine;
using UnityEngine.VFX;

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
        private ReactiveProperty<Vector2?> _position = new ReactiveProperty<Vector2?>();
        public IReadOnlyReactiveProperty<Vector2?> Position => _position;

        // unit�̖��O
        private ReactiveProperty<string> _name = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> Name => _name;

        // �w�c[0:���� 1:�G 2:������]
        private ReactiveProperty<int> _faction = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Faction => _faction;

        public void SetPosition(Vector2? value)
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