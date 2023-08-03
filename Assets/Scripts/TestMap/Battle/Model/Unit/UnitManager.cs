using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Battle.Model.Unit
{
    /// <summary>
    /// �}�b�v��ɑ��݂��Ă��郆�j�b�g�̌����A�X�V
    /// </summary>
    public class UnitManager
    {
        private List<Battle.Unit> _units;

        /// <summary>
        /// ��������郆�j�b�g���X�g��Ⴂ���j�b�g�𐶐�����
        /// </summary>
        public UnitManager(List<Battle.Unit> units)
        {
            _units = new List<Battle.Unit>();
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

        // ����̐w�c�̃��j�b�g��S�Ď擾
        public List<Battle.Unit> GetUnitsByFaction(int faction)
        {
            return _units.Where(unit => unit.Faction.Value == faction).ToList();
        }
    }
}