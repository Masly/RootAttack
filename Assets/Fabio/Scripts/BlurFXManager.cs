using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurFXManager : MonoBehaviour
{
    Volume _volume;
    DepthOfField _dof;

    public static BlurFXManager i;


    private void Awake()
    {
        i = this;

        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _dof);

    }

    void Start()
    {

    }

    public void ShowBlurEffect()
    {
        _dof.active = true;
    }

    public void HideBlurEffect()
    {
        _dof.active = false;

    }
}