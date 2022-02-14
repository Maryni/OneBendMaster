using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BaseBullet
{
    

    protected override void SetBullet(float damage, PatronElementType patronElementType)
    {
        base.SetBullet(this.damage, PatronElementType.Fire);
    }


}
