using UnityEngine;
using UnityEngine.VR;
using System.Collections;
#if UNITY_PS4
using UnityEngine.PS4.VR;
using UnityEngine.PS4;
#endif

public class VRManager : Singleton<VRManager>
{
    private float renderScale = 1.4f; // 1.4 is Sony's recommended scale for PlayStation VR

    private bool showHmdViewOnMonitor = true; // Set this to 'false' to use the monitor/display as the Social Screen

    new void Awake()
    {
        Debug.Log("------------- VRManager Instance");
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void BeginVRSetup()
    {
        Debug.Log("BeginVRSetup");
        StartCoroutine(SetupVR());
    }

    IEnumerator SetupVR()
    {
#if UNITY_PS4
        Debug.Log("SetupVR");
        // Register the callbacks needed to detect resetting the HMD
		Utility.onSystemServiceEvent += OnSystemServiceEvent;
        PlayStationVR.onDeviceEvent += onDeviceEvent;

        // Post-reproject for camera locked items, in this case the reticle. Must be
        // set before we change the VR Device. See VRPostReprojection.cs for more info
        if (Camera.main.actualRenderingPath == RenderingPath.Forward)
        {
            if (FindObjectOfType<VRPostReprojection>())
            {
                PlayStationVRSettings.postReprojectionType = PlayStationVRPostReprojectionType.PerEye;
            }
            else
            {
                Debug.LogError("You are trying to enable support for post-reprojection, but no post-reprojection script was found!");
            }
        }
        else
        {
            Debug.LogError("Post reprojection is not yet fully supported in non-Forward Rendering Paths.");
        }
#endif

        VRSettings.LoadDeviceByName(VRDeviceNames.PlayStationVR);

        // WORKAROUND: At the moment the device is created at the end of the frame so
        // changing almost any VR settings needs to be delayed until the next frame
        yield return null;

        Debug.Log("------------->>> Loaded Device Name  <<< --------------" + VRSettings.loadedDeviceName.ToString() );

        VRSettings.enabled = true;
        VRSettings.renderScale = renderScale;
        VRSettings.showDeviceView = showHmdViewOnMonitor;

        InputTracking.Recenter();

        GameFadeManager.Instance.SetupVRMode(Camera.main.name);
    }

    public void BeginShutdownVR()
    {
        Debug.Log("BeginShotdownVR");
        StartCoroutine(ShutdownVR());
    }

    IEnumerator ShutdownVR()
    {
        VRSettings.LoadDeviceByName(VRDeviceNames.None);

        // WORKAROUND: At the moment the device is created at the end of the frame so
        // we need to wait a frame until the VR device is changed back to 'None', and
        // then reset the Main Camera's FOV and Aspect
        yield return null;

        VRSettings.enabled = false;
        VRSettings.showDeviceView = false;

#if UNITY_PS4
        // Unregister the callbacks needed to detect resetting the HMD
        Utility.onSystemServiceEvent -= OnSystemServiceEvent;
        PlayStationVR.onDeviceEvent -= onDeviceEvent;
//        PlayStationVR.setOutputModeHMD(false, false);
        PlayStationVR.SetOutputModeHMD(false);
#endif
        Camera.main.ResetFieldOfView();
        Camera.main.ResetAspect();

        GameFadeManager.Instance.ResetVRMode();
    }

    public void SetupHMDDevice()
    {
#if UNITY_PS4
        // The HMD Setup Dialog is not displayed on the social screen in separate
        // mode, so we'll force it to mirror-mode first
        VRSettings.showDeviceView = false;

        // Show the HMD Setup Dialog, and specify the callback for when it's finished
        HmdSetupDialog.OpenAsync(0, OnHmdSetupDialogCompleted);
#endif
    }

    public void ToggleHMDViewOnMonitor(bool showOnMonitor)
    {
        showHmdViewOnMonitor = showOnMonitor;
        VRSettings.showDeviceView = showHmdViewOnMonitor;
    }

    public void ToggleHMDViewOnMonitor()
    {
        showHmdViewOnMonitor = !showHmdViewOnMonitor;
        VRSettings.showDeviceView = showHmdViewOnMonitor;
    }

    public void ChangeRenderScale(float scale)
    {
        VRSettings.renderScale = scale;
    }

#if UNITY_PS4
    // HMD recenter happens in this event
    void OnSystemServiceEvent(UnityEngine.PS4.Utility.sceSystemServiceEventType eventType)
    {
        Debug.LogFormat("OnSystemServiceEvent: {0}", eventType);

        switch (eventType)
        {
            case Utility.sceSystemServiceEventType.RESET_VR_POSITION:
                InputTracking.Recenter();
                break;
        }
    }
#endif

#if UNITY_PS4
    // Detect completion of the HMD dialog and either proceed to setup VR, or throw a warning
    void OnHmdSetupDialogCompleted(DialogStatus status, DialogResult result)
    {
        Debug.LogFormat("OnHmdSetupDialogCompleted: {0}, {1}", status, result);

        switch (result)
        {
            case DialogResult.OK:
                Debug.Log("------------>>>>>>>>>>> HMD Setup Dialog DialogResult.OK ---> SetupVR");
                StartCoroutine(SetupVR());
                break;
            case DialogResult.UserCanceled:
                Debug.LogWarning("User Cancelled HMD Setup!");
                BeginShutdownVR();
                break;
        }
    }
#endif

#if UNITY_PS4
    // This handles disabling VR in the event that the HMD has been disconnected
    bool onDeviceEvent(PlayStationVR.deviceEventType eventType, int value)
    {
        Debug.LogFormat("### onDeviceEvent: {0}, {1}", eventType, value);
        bool handledEvent = false;

        switch (eventType)
        {
            case PlayStationVR.deviceEventType.deviceStopped:
                BeginShutdownVR();
                handledEvent = true;
                break;
            case PlayStationVR.deviceEventType.StatusChanged:   // e.g. HMD unplugged
                VRDeviceStatus devstatus = (VRDeviceStatus)value;
                Debug.LogFormat("DeviceStatus: {0}", devstatus);
                if (devstatus != VRDeviceStatus.Ready)
                {
                    // TRC R4026 suggests showing the HMD Setup Dialog if the device status becomes non-ready
                    if (VRSettings.loadedDeviceName == VRDeviceNames.None)
                        SetupHMDDevice();
                    else
                        BeginShutdownVR();
                }
                handledEvent = true;
                break;
            case PlayStationVR.deviceEventType.MountChanged:
                VRHmdMountStatus status = (VRHmdMountStatus)value;
                Debug.LogFormat("VRHmdMountStatus: {0}", status);
                handledEvent = true;
                break;
        }

        return handledEvent;
    }
#endif

    /**
     *      VRモードと通常モードを切り替える
     */
    public void ChangeVRMode()
    {
        Debug.Log("------------- Begin SetupHMD Device");
        SetupHMDDevice();
    }
}
