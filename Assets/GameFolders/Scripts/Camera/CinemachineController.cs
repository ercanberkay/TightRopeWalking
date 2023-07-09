using Managers;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineController : MonoBehaviour
{
    public static CinemachineController instance;
    public CameraFollowTarget followTarget;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineTransposer _transposer;
    private CinemachineComposer _composer;
    

    private void Awake()
    {
        instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    public void SetTarget(CameraFollowTarget target)
    {
        followTarget = target;
        ChangeTarget(followTarget.transform);
    }

    public void SetConfig(CameraConfig config)
    {
        ChangeCamPosInTime(config.Position,.5f,false);
        ChangeCamRotInTime(config.Rotation, .5f, false);
    }

    public void ChangeCamPosInTime(Vector3 target, float duration, bool isAddition = true)
    {
        Vector3 pos = isAddition ? _transposer.m_FollowOffset + target : target;
        DOTween.To(() => _transposer.m_FollowOffset, x => _transposer.m_FollowOffset = x, pos, duration);
    }

    public void ChangeCamRotInTime(Vector3 target, float duration, bool isAddition = true)
    {
        var rot = isAddition ? _composer.m_TrackedObjectOffset + target : target;
        DOTween.To(() => _composer.m_TrackedObjectOffset, x => _composer.m_TrackedObjectOffset = x, rot, duration);
    }

    private void ChangeTarget(Transform targetTransform)
    {
        //_virtualCamera.m_LookAt = targetTransform;
        _virtualCamera.m_Follow = targetTransform;
    }

}
