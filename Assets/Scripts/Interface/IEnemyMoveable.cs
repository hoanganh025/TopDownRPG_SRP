using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveable
{
    void EnemyMove(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
