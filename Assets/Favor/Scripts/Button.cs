using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : ObjectBase, IPushable
{
    private int massLimit;
    
    public int MassLimit
    {
        get 
        {
            return massLimit;
        }
        set
        {
            if(massLimit != value)
            {
                massLimit = value;
            }
        }
    }

    public void Push()
    {
        // 눌려용
    }
    
    /*
     rpg
    플레이어.

    void PlayerUse(IUsable item)
    {
       item.Use();
    }

    class inventory
    {
        item[] items;
        Player player;

        UseItem(int index)
        {
            player.PlayerUse(items[index]);
        }
        
    }


    아이템 IUsable {Use()}
    스킬
    class Item : IUsable
    {
        int 갯수;
        bool 사용가능?
        void Use(){ 먹어용;}
    }
     
     */
    // 충돌 처리
}
