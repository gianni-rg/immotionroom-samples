﻿/************************************************************************************************************
 * 
 * Copyright (C) 2014-2017 Immotionar, a division of Beps Engineering.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************************************************/

using ImmotionAR.ImmotionRoom.LittleBoots.Avateering.Collisions;
using UnityEngine;

/// <summary>
/// Manages the behaviour of a crazy big ball
/// </summary>
public class CrazyBigBall : MonoBehaviour 
{

	void OnTriggerEnter(Collider collider)
    {
        //if we collided with an avatar hand or foot
        if (AvatarCollidersProps.IsAvatarCollider(collider))
        {
            //get foot or hand position, ball position and go somewhere super-crazy in that direction
            GetComponent<Rigidbody>().AddForce((transform.position - collider.bounds.center) * (Random.value * 10 + 2) + Random.value * 1.7f * Vector3.up, ForceMode.Impulse);
        }
            
    }
}
