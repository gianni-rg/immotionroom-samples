/************************************************************************************************************
 * 
 * Copyright (C) 2014-2016 ImmotionAR, a division of Beps Engineering.
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
/// Manages the behaviour of a crazy soccer ball
/// </summary>
public class CrazySoccerBall : MonoBehaviour 
{

	void OnTriggerEnter(Collider collider)
    {
        //if we collided with the avatar foot
        if (AvatarCollidersProps.IsAvatarFootCollider(collider))
        {
            //get foot position, ball position and go somewhere crazy in that direction
            GetComponent<Rigidbody>().AddForce((transform.position - collider.bounds.center) * (Random.value * 20 + 10) + Random.value * 10 * Vector3.up, ForceMode.Impulse);
        }
            
    }
}
