using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PushableObject : ObjectBase
{
    protected int massLimit;

    protected int totalMass;
    protected int TotalMass
    {
        get
        {
            return totalMass;
        }
        set
        {
            if(totalMass!=value)
            {
                totalMass = value;

                if (CanPush())
                    Push();
            }
        }
    }

    protected abstract void Push();

    protected bool CanPush()
    {
        return TotalMass > massLimit;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(TryGetComponent(out ObjectBase obj))
        {
            TotalMass += obj.mass;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (TryGetComponent(out ObjectBase obj))
        {
            TotalMass -= obj.mass;
        }
    }
}
