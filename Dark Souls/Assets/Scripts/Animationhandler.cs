using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationhandler : MonoBehaviour
{
    public Animator anim;
    int vertical;
    int horizontal;

    public bool canRotate;

    public void Initialize()
    {
        anim = GetComponent<Animator>();
        vertical = Animator.StringToHash("vertical");

        // NOTE: Received an error that animator does not have a definition for stringtoHash func, looked up the error.
        // One of the suggestions was existence of another animator class, noticed the name of the script was "animator' so changed it to animation handler and it works now

        horizontal = Animator.StringToHash("horizontal");

        // Also vertical and horizonatal are taking in parameter id for both vertical and horizontal, setter and getter which allows control over param value
        //basically controlling vertical and horizontal param valuues

    }

    public void UpdateAnimatorValues (float verticalMovement, float horizontalMovement)
    {
        #region Vertical

        float v = 0;

        if (verticalMovement>0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }

        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }

        else if(verticalMovement <0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }

        else if (verticalMovement < -0.55f)
        {
            v = -1;

        }

        else
        {
            v = 0;
        }

        #endregion
        //These parameters are added in the parameters tab of animator
        #region horizontal

        float h = 0;

        if(horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }

        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }

        else if (horizontalMovement <0 && horizontalMovement > -0.55f)
        {
            h= -0.5f;
        }

        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }

        else
        {
            h = 1;
        }

        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void stopRotation()
    {
        canRotate = false;
    }
}
