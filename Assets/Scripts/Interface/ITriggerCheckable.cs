using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool isChaseTrigger { get; set; }
    bool isAttackTrigger { get; set; }
    void SetChaseStatus(bool chaseStatus);
    void SetAttackStatus(bool attackStatus);
}
