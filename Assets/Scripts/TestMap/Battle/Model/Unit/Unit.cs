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

        // 攻撃力
        private ReactiveProperty<int> _attack = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Attack => _attack;

        // ユニットの位置
        private ReactiveProperty<Vector2Int> _position = new ReactiveProperty<Vector2Int>();
        public IReadOnlyReactiveProperty<Vector2Int> Position => _position;

        // unitの名前
        private ReactiveProperty<string> _name = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> Name => _name;

        // 陣営[0:味方 1:敵 2:無所属]
        private ReactiveProperty<int> _faction = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Faction => _faction;

        private ReactiveProperty<int> _actionPoint = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> ActionPoint => _actionPoint;

        // ユニットの行動
        // ユニットが出来る行動(他のユニットやパラメーターに影響を及ぼすもの)は全て行動して表現され、ここに格納される
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
            //デバッグログ
            Debug.Log($"{_name.Value} - ActionPoint:{_actionPoint.Value}");
        }

        public void SetPosition(Vector2Int value)
        {
            _position.Value = value;
            //デバッグログ
            Debug.Log($"{_name.Value} - Position:{_position.Value}");
        }

        public void SetId(int value)
        {
            _id.Value = value;
            //デバッグログ
            Debug.Log($"{_name.Value} - ID:{_id.Value}");
        }

        // HPと攻撃力を設定するためのメソッド
        public void SetHP(int value)
        {
            _hp.Value = value;
            //デバッグログ
            Debug.Log($"{_name.Value} - HP:{_hp.Value}");
        }

        public void SetAttack(int value)
        {
            _attack.Value = value;
            //デバッグログ
            Debug.Log($"{_name.Value} - Attack:{_attack.Value}");

        }

        //名前を設定
        public void SetName(string value)
        {
            _name.Value = value;
            //デバッグログ
            Debug.Log($"{_name.Value} - Name:{_name.Value}");
        }

        //陣営を設定
        public void SetFaction(int value)
        {
            _faction.Value = value;
            //デバッグログ
            Debug.Log($"{_name.Value} - Faction:{_faction.Value}");
        }

        public void TakeDamage(int damage)
        {
            _hp.Value -= damage;

            // HPが0以下になったら、0にクリップする（体力がマイナスになるのを防ぐ）
            _hp.Value = Mathf.Max(_hp.Value, 0);

            Debug.Log($"{_name.Value} - Took {damage} damage, remaining HP: {_hp.Value}");
        }

    }
}