using System.Collections.Generic;

namespace ProjectX.Battle.Model.Unit
{
    /// <summary>
    /// マップ上に存在しているユニットの検索、更新
    /// </summary>
    public class UnitManager
    {
        private List<Battle.Unit> _units = new List<Battle.Unit>();

        /// <summary>
        /// 生成されるユニットリストを貰いユニットを生成する
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