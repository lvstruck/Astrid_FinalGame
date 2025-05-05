using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Fixcamera : MonoBehaviour
{
 
    public float FarDistance = 4.5f;
    public float NearDistance = 1.5f;

    CinemachineFramingTransposer m_Transposer;
    CinemachineComposer m_Composer;

    float m_SavedAimScreenX;
    float m_SavedBodyScreenX;

    public void FixingCamera(CinemachineVirtualCameraBase vcamBase, ref CameraState curState, float deltaTime)
    {
        var vcam = vcamBase as CinemachineVirtualCamera;
        if (m_Transposer == null)
            m_Transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (m_Composer == null)
            m_Composer = vcam.GetCinemachineComponent<CinemachineComposer>();

        m_SavedBodyScreenX = m_Transposer.m_ScreenX;
        m_SavedAimScreenX = m_Composer.m_ScreenX;

        // If the LookAt and Follow targets are close enough, bring the
        // screen positions towards the center
        var p1 = vcam.Follow.position; p1.y = 0;
        var p2 = curState.ReferenceLookAt; p2.y = 0;
        var d = Vector3.Distance(p1, p2);
        if (d < FarDistance)
        {
            // Maybe change this to add some easing
            float t = Mathf.Max(0, (d - NearDistance) / (FarDistance - NearDistance));

            m_Transposer.m_ScreenX = 0.5f + (m_Transposer.m_ScreenX - 0.5f) * t;
            m_Composer.m_ScreenX = 0.5f + (m_Composer.m_ScreenX - 0.5f) * t;
        }
    }
    protected void FixiCamera(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            m_Transposer.m_ScreenX = m_SavedBodyScreenX;
            m_Composer.m_ScreenX = m_SavedAimScreenX;
        }
    }
}
