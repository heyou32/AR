using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Drive : MonoBehaviour
{
    public WheelCollider[] WCs;
    public GameObject[] Wheels;

    public AudioSource driveSource, brakeSource;
    public SoundType state;

    float torque = 10;
    public float maxSteerAngle = 30;
    public float brake = 0;
    public float thrustTorque;
    public float accelValue = 0;
    public float steerValue = 0;

    public bool ChangeGear = true;
    public bool _brake = false;
    public bool _accele = false;
    public bool isAccelButtonDown = false;
    public bool isSteerLeftDown = false;
    public bool isSteerRightDown = false;
    public bool isBrakeButtonDown = false;
    public bool isChangeGear = true;

    public enum SoundType
    {
        off,
        drive
    }

    private void Start()
    {
        UpDownEventRegister("Canvas/LeftButton",
            data => LeftButtonDown(),
            data => LeftButtonUp());

        UpDownEventRegister("Canvas/RightButton",
           data => RightButtonDown(),
           data => RightButtonUp());

        UpDownEventRegister("Canvas/AccelButton",
         data => AccelButtonDown(),
         data => AccelButtonUp());

        UpDownEventRegister("Canvas/BrakeButton",
          data => BrakeButtonDown(),
          data => BrakeButtonUp());

        ClickEventRegister("Canvas/GearButton",
            data => Change());
    }

    private void BrakeButtonUp()
    {
        isBrakeButtonDown = false;
    }

    private void BrakeButtonDown()
    {
        isBrakeButtonDown = true;
    }

    private void AccelButtonUp()
    {
        isAccelButtonDown = false;
    }

    private void AccelButtonDown()
    {
        isAccelButtonDown = true;
    }

    private void RightButtonUp()
    {
        isSteerRightDown = false;
    }

    private void RightButtonDown()
    {
        isSteerRightDown = true;
    }

    private void LeftButtonUp()
    {
        isSteerLeftDown = false;
    }

    private void LeftButtonDown()
    {
        isSteerLeftDown = true;
    }

    public void UpDownEventRegister(string name, UnityAction<BaseEventData> downCallback, UnityAction<BaseEventData> upCallback)
    {
        GameObject btn = GameObject.Find(name);
        EventTrigger et = btn.GetComponent<EventTrigger>();

        List<EventTrigger.Entry> ents = et.triggers;
        foreach (var ent in ents)
        {
            if (ent.eventID == EventTriggerType.PointerDown)
            {
                ent.callback.AddListener(downCallback);
            }
            if (ent.eventID == EventTriggerType.PointerUp)
            {
                ent.callback.AddListener(upCallback);
            }
        }
    }

    public void ClickEventRegister(string name, UnityAction<BaseEventData> clickCallback)
    {
        GameObject btn = GameObject.Find(name);
        EventTrigger et = btn.GetComponent<EventTrigger>();

        List<EventTrigger.Entry> ents = et.triggers;
        foreach (var ent in ents)
        {
            if (ent.eventID == EventTriggerType.PointerClick)
            {
                ent.callback.AddListener(clickCallback);
            }
        }
    }

    public void Sfx()   // Sound fx
    {
        float motorTorque = WCs[0].rpm;
        float brakeTorque = WCs[0].brakeTorque;
        switch (state)
        {
            case SoundType.drive:
                if (motorTorque > 1)
                {
                    if (brakeTorque > 0)
                    {
                        if (brakeSource.isPlaying == false)
                        {
                            driveSource.Stop();
                            brakeSource.Play();
                        }
                    }
                    else
                        if (driveSource.isPlaying == false)
                        driveSource.Play();
                }
                else
                {
                    state = SoundType.off;
                    driveSource.Stop();
                    brakeSource.Stop();
                }
                break;

            case SoundType.off:
                if (motorTorque > 0)
                    state = SoundType.drive;
                break;
        }
    }

    public void Change() // 전진&후진기어 변환
    {
        if (isChangeGear)
            ChangeGear = !ChangeGear;
    }

    public void Braking() // 휠 브레이크 회전력 제어
    {
        if (isBrakeButtonDown)
        {
            for (int i = 0; i < 4; i++)
            {
                WCs[i].brakeTorque = 500;
            }
        }
        else
            for (int i = 0; i < 4; i++)
            {
                WCs[i].brakeTorque = 0;
            }
    }

    public void Steering() // 휠 회전값 제어
    {
        if (isSteerLeftDown)
        {
            steerValue = Mathf.Clamp(steerValue - 0.1f, -1, 0) * maxSteerAngle;
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                    WCs[i].steerAngle = steerValue;
                // 휠 운동 시각화
                Quaternion quat;
                Vector3 position;
                WCs[i].GetWorldPose(out position, out quat);

                Wheels[i].transform.position = position;
                Wheels[i].transform.rotation = quat;
            }
        }
        else if (isSteerRightDown)
        {
            steerValue = Mathf.Clamp(steerValue + 0.1f, 0, 1) * maxSteerAngle;
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                    WCs[i].steerAngle = steerValue;
                // 휠 운동 시각화
                Quaternion quat;
                Vector3 position;
                WCs[i].GetWorldPose(out position, out quat);

                Wheels[i].transform.position = position;
                Wheels[i].transform.rotation = quat;
            }
        }
        else
        {
            steerValue = Mathf.Clamp(steerValue + 0.1f, 0, 0) * maxSteerAngle;
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                    WCs[i].steerAngle = steerValue;
                // 휠 운동 시각화
                Quaternion quat;
                Vector3 position;
                WCs[i].GetWorldPose(out position, out quat);

                Wheels[i].transform.position = position;
                Wheels[i].transform.rotation = quat;
            }
        }
    }

    public void motorTorque()
    {
        thrustTorque = accelValue * torque;
        for (int i = 0; i < 4; i++)
        {
            WCs[i].motorTorque = thrustTorque;
        }
    }
    public void Accelerating() // 휠 가속도 제어
    {
        if (isAccelButtonDown)
        {
            if (ChangeGear)
                accelValue = Mathf.Clamp(accelValue + 0.05f, 0, 1);
            else if (!ChangeGear)
                accelValue = Mathf.Clamp(accelValue - 0.05f, -1, 0);
        }
        else
            accelValue = Mathf.Clamp(accelValue - 0.5f, 0, 1);
    }

    void Update()
    {
        Accelerating();
        motorTorque();
        Steering();
        Braking();
        Sfx();
    }
}