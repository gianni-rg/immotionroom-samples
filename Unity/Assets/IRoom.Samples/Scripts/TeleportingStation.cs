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
using ImmotionAR.ImmotionRoom.LittleBoots.VR.PlayerController;
using UnityEngine;

/// <summary>
/// Teleporting Station: when the player enters it, it gets teleported in another position
/// </summary>
public class TeleportingStation : MonoBehaviour
{
    #region Public Unity Properties

    /// <summary>
    /// Position to teleport the player to
    /// </summary>
    [Tooltip("Position to teleport the player to")]
    public Vector3 TeleportingPosition;

    #endregion

    #region Private fields

    /// <summary>
    /// Reference to the player controller
    /// </summary>
    IroomPlayerController m_playerController;

    /// <summary>
    /// Reference to collider of this object
    /// </summary>
    Collider m_collider;

    #endregion

    #region Behaviour methods

    void Start()
    {
        m_playerController = FindObjectOfType<IroomPlayerController>();
        m_collider = GetComponent<Collider>();
    }

	void Update () 
    {
        //if player is entering the teleport, teleport it!
        if(m_playerController.IsVrReady && m_collider.bounds.Contains(m_playerController.CharController.transform.position))
        {
            m_playerController.CharController.transform.position = TeleportingPosition;
        }
    }

    #endregion
}
