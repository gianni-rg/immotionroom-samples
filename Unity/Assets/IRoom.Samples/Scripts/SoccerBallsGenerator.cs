/************************************************************************************************************
 * 
 * Copyright (C) 2014-2017 Immotionar, a division of Beps Engineering. All rights reserved.
 * 
 * Licensed under the ImmotionAR ImmotionRoom SDK License (the "License");
 * you may not use the ImmotionAR ImmotionRoom SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 * 
 * You may obtain a copy of the License at
 * 
 * http://www.immotionar.com/legal/ImmotionRoomSDKLicense.PDF
 * 
 ************************************************************************************************************/

using UnityEngine;
using System.Collections;
using ImmotionAR.ImmotionRoom.LittleBoots.VR.PlayerController;

/// <summary>
/// Makes a coloured crazy soccer ball to fall from the sky from time to time
/// </summary>
public class SoccerBallsGenerator : MonoBehaviour
{
    #region Public Unity Properties

    /// <summary>
    /// Ball to generate
    /// </summary>
    [Tooltip("Ball object to generate")]
    public GameObject CrazyBall;

    /// <summary>
    /// Time between consecutive generations of a ball, in seconds
    /// </summary>
    [Tooltip("Ball object to generate")]
    public float GenerationTimeInterval = 5;

    /// <summary>
    /// Maximum distance, on the XZ plane, from the generator and the generated ball
    /// </summary>
    [Tooltip("Maximum distance, on the XZ plane, from the generator and the generated ball")]
    public float GenerationRadius = 5;

    #endregion

    #region Behaviour methods

    // Use this for initialization
	void Start () 
    {
        StartCoroutine("BallGeneration");
    }

    #endregion

    #region Private methods

    private IEnumerator BallGeneration()
    {
        //wait for player controller to be ready
        IroomPlayerController player = FindObjectOfType<IroomPlayerController>();

        while(!player.IsVrReady)
            yield return new WaitForSeconds(0.5f);

        //then, generate balls forever!
        while(true)
        {
            //generate a ball, as child of this object
            GameObject ballGo = Instantiate<GameObject>(CrazyBall);
            ballGo.transform.SetParent(transform, false);

            //assign it generator y pos and a random XZ position, inside the requested distance
            Vector2 randomXZpos = new Vector2(player.MainAvatar.BodyRootJoint.transform.position.x, player.MainAvatar.BodyRootJoint.transform.position.z) + GenerationRadius * Random.insideUnitCircle;
            ballGo.transform.localPosition = new Vector3(randomXZpos.x, ballGo.transform.localPosition.y, randomXZpos.y);

            //assign it a random color
            ballGo.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            //let the ball have its life and wait for next generation
            yield return new WaitForSeconds(GenerationTimeInterval);
        }
    }

    #endregion
}
