using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] VariableJoystick joystick;
    [SerializeField] Player player;
    [SerializeField] CameraFollow cameraFollow;
    public void OnInit()
    {
        UIManager.Instance.ShowScreen<ScreenGame>();
        cameraFollow.SetTarget(player.transform);
        player.OnInit();
        player.GetCom<PlayerMove>().SetJoyStick(joystick);
        player.StartLevel();
    }


    private void Update()
    {
        player.OnUpdate(Time.deltaTime);
    }
}
