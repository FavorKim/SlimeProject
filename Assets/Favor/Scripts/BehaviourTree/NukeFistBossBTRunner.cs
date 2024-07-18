using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeFistBossBTRunner
{
    NukeFistBoss owner;
    Node rootNode;
    public NukeFistBossBTRunner(NukeFistBoss owner)
    {
        this.owner = owner;
        SetBT();
    }

    public void RunBT()
    {
        rootNode?.Evaluate();
    }

    void SetBT()
    {
        rootNode = new Selector(new List<Node>
        {
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckIsDead(owner),
                    new Dead(owner,owner.InvokeOnDead)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIsHit(owner),
                    new Hit(owner, owner.InvokeOnHit)
                })
            }),
            new Selector(new List<Node>
            {
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckIsInRange(owner,owner.CheckIsInRange),
                        new Attack(owner,owner.InvokeOnAttack)
                    })
                }),
                new Sequence(new List<Node>
                {
                    new CheckCanMove(owner,owner.CheckCanMove),
                    new Move(owner,owner.InvokeOnMove)
                })
            })
        }) ;
    }
}
