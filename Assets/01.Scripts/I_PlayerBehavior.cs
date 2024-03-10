using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_PlayerBehavior
{
    void I_Attack(float damage);
    void I_Damage(float damage);

    void I_Die();

    void I_Shield(float damage);
}
