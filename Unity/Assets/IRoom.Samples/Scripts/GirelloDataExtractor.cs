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


using UnityEngine;
using ImmotionAR.ImmotionRoom.LittleBoots.VR.Girello;
using ImmotionAR.ImmotionRoom.LittleBoots.VR.PlayerController;


/// <summary>
/// Gets data of game play are (Girello) and puts some objects pose and dimension accordingly
/// </summary>
public class GirelloDataExtractor : MonoBehaviour 
{

    #region Behaviour methods

	// Update is called once per frame
	void Update ()
    {
        //if the player is already calibrated
        var playerController = FindObjectOfType<IroomPlayerController>();

        if (playerController.IsVrReady)
        {
            //get girello data from it
            GirelloData gd = playerController.GirelloData;

            //assign to the cube the exact pose of girello. This way the cube reflect exactly the game area box
            transform.GetChild(0).position = gd.Center;
            transform.GetChild(0).rotation = gd.Rotation;
            transform.GetChild(0).localScale = gd.Size;

            //put the sphere on a side of the box, at medium height
            transform.GetChild(1).position = gd.Center + gd.Rotation * new Vector3(gd.Size.x / 2, 0, gd.Size.z / 2);
        }
	}

    #endregion
}
