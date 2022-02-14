using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : BaseBullet
{
    private float damage;

    protected override void SetBullet(float damage, PatronElementType patronElementType)
    {
        base.SetBullet(this.damage, PatronElementType.Magic);
    }


}
