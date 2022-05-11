using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatPlayer : MonoBehaviour
{
    private Sprite[] anim_idle_d;

    private Sprite[] anim_idle_dl;

    private Sprite[] anim_idle_l;

    private Sprite[] anim_idle_lu;

    private Sprite[] anim_idle_u;

    private Sprite[] anim_idle_ur;

    private Sprite[] anim_idle_r;

    private Sprite[] anim_idle_rd;

    private Sprite[] anim_walk_d;

    private Sprite[] anim_walk_dl;

    private Sprite[] anim_walk_l;

    private Sprite[] anim_walk_lu;

    private Sprite[] anim_walk_u;

    private Sprite[] anim_walk_ur;

    private Sprite[] anim_walk_r;

    private Sprite[] anim_walk_rd;

    private Sprite[] anim_current;

    private Vector2 anim_direction = Vector2.zero;

    private bool isWalking = false;

    private bool isRenderFrame = true;

    private int index = 0;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector2[]
        directions =
            new Vector2[] {
                Vector2.right,
                Vector2.right + Vector2.up,
                Vector2.up,
                Vector2.left + Vector2.up,
                Vector2.left,
                Vector2.left + Vector2.down,
                Vector2.down,
                Vector2.right + Vector2.down
            };

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        LoadAnimationsFromMods();
        anim_current = anim_idle_d;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (GetDirectionAnim() != anim_current)
        {
            index = 0;
        }
        anim_current = GetDirectionAnim();

        if (anim_current != null && isRenderFrame)
        {
            spriteRenderer.sprite = anim_current[index];
            index++;

            if (index == anim_current.Length)
            {
                index = 0;
            }

            isRenderFrame = false;
        }
        else
        {
            isRenderFrame = true;
        }
    }

    private void LoadAnimationsFromMods()
    {
        anim_idle_d = AnimatManager.instance.GetAnimation("cid_idle_d");
        anim_idle_dl = AnimatManager.instance.GetAnimation("cid_idle_dl");
        anim_idle_l = AnimatManager.instance.GetAnimation("cid_idle_l");
        anim_idle_lu = AnimatManager.instance.GetAnimation("cid_idle_lu");
        anim_idle_u = AnimatManager.instance.GetAnimation("cid_idle_u");
        anim_idle_ur = AnimatManager.instance.GetAnimation("cid_idle_ur");
        anim_idle_r = AnimatManager.instance.GetAnimation("cid_idle_r");
        anim_idle_rd = AnimatManager.instance.GetAnimation("cid_idle_rd");

        anim_walk_d = AnimatManager.instance.GetAnimation("cid_walk_d");
        anim_walk_dl = AnimatManager.instance.GetAnimation("cid_walk_dl");
        anim_walk_l = AnimatManager.instance.GetAnimation("cid_walk_l");
        anim_walk_lu = AnimatManager.instance.GetAnimation("cid_walk_lu");
        anim_walk_u = AnimatManager.instance.GetAnimation("cid_walk_u");
        anim_walk_ur = AnimatManager.instance.GetAnimation("cid_walk_ur");
        anim_walk_r = AnimatManager.instance.GetAnimation("cid_walk_r");
        anim_walk_rd = AnimatManager.instance.GetAnimation("cid_walk_rd");
    }

    private Sprite[] GetDirectionAnim()
    {
        if (anim_direction == Vector2.down)
        {
            if (isWalking)
            {
                return anim_walk_d;
            }
            else
            {
                return anim_idle_d;
            }
        }
        if (anim_direction == Vector2.left + Vector2.down)
        {
            if (isWalking)
            {
                return anim_walk_dl;
            }
            else
            {
                return anim_idle_dl;
            }
        }
        if (anim_direction == Vector2.left)
        {
            if (isWalking)
            {
                return anim_walk_l;
            }
            else
            {
                return anim_idle_l;
            }
        }
        if (anim_direction == Vector2.left + Vector2.up)
        {
            if (isWalking)
            {
                return anim_walk_lu;
            }
            else
            {
                return anim_idle_lu;
            }
        }
        if (anim_direction == Vector2.up)
        {
            if (isWalking)
            {
                return anim_walk_u;
            }
            else
            {
                return anim_idle_u;
            }
        }
        if (anim_direction == Vector2.right + Vector2.up)
        {
            if (isWalking)
            {
                return anim_walk_ur;
            }
            else
            {
                return anim_idle_ur;
            }
        }
        if (anim_direction == Vector2.right)
        {
            if (isWalking)
            {
                return anim_walk_r;
            }
            else
            {
                return anim_idle_r;
            }
        }
        if (anim_direction == Vector2.right + Vector2.down)
        {
            if (isWalking)
            {
                return anim_walk_rd;
            }
            else
            {
                return anim_idle_rd;
            }
        }

        return null;
    }

    public void SetAnimDirection(Vector2 direction)
    {
        anim_direction = RoundDirection(direction);
    }

    private bool IsVectorClose(Vector2 v1, Vector2 v2)
    {
        if (Vector2.Distance(v1, v2) < 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 RoundDirection(Vector2 toRound)
    {
        Vector2 closest = Vector2.zero;
        foreach (Vector2 direction in directions)
        {
            if (
                Vector2.Distance(toRound, direction) <
                Vector2.Distance(toRound, closest)
            )
            {
                closest = direction;
            }
        }

        return closest;
    }

    public void SetIsWalking(bool isWalking)
    {
        this.isWalking = isWalking;
    }
}
