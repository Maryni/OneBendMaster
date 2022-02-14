using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureBullet : BaseBullet
{
    private float damage;

    protected override void SetBullet(float damage, PatronElementType patronElementType)
    {
        base.SetBullet(this.damage, PatronElementType.Nature);
    }


}
