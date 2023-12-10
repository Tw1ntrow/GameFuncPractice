using System.Collections.Generic;
using UnityEngine;

public interface IMapCreatable
{
    public Dictionary<Vector2Int, ProjectX.Battle.Grid> GetMap();

}
