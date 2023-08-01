using System.Collections.Generic;

namespace ProjectX.Battle.Model.Unit
{
    /// <summary>
    /// �}�b�v��ɑ��݂��Ă��郆�j�b�g�̌����A�X�V
    /// </summary>
    public class UnitManager
    {
        private List<Battle.Unit> _units = new List<Battle.Unit>();

        /// <summary>
        /// ��������郆�j�b�g���X�g��Ⴂ���j�b�g�𐶐�����
        /// </summary>
        public UnitManager(List<Battle.Unit> units)
        {
            _units = units;
        }

        public void AddUnit(Battle.Unit unit)
        {
            _units.Add(unit);
        }

        public void RemoveUnit(Battle.Unit unit)
        {
            _units.Remove(unit);
        }

        public Battle.Unit GetUnit(int id)
        {
            return _units.Find(unit => unit.Id.Value == id);
        }

        public List<Battle.Unit> GetUnits()
        {
            return _units;
        }
    }
}