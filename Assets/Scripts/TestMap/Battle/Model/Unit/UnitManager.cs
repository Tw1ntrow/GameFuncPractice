using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Battle.Model.Unit
{
    /// <summary>
    /// マップ上に存在しているユニットの検索、更新
    /// </summary>
    public class UnitManager
    {
        private List<Battle.Unit> _units;

        /// <summary>
        /// 生成されるユニットリストを貰いユニットを生成する
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

        // 特定の陣営のユニットを全て取得
        public List<Battle.Unit> GetUnitsByFaction(int faction)
        {
            return _units.Where(unit => unit.Faction.Value == faction).ToList();
        }
    }
}