using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : PlayerController
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        // 나중에 네트워크를 통해 다른 플레이어들 조종 할 수 있어야한다.
        switch (State)
        {
            case CreatureState.IDLE:
                GetDirInput();
                break;
            case CreatureState.MOVING:
                GetDirInput();
                break;
        }

        base.UpdateController();
    }

    protected override void UpdateIdle()
    {
        // 이동 상태로 갈지 확인
        if (Dir != MoveDir.NONE)
        {
            State = CreatureState.MOVING;
            return;
        }
    }

    // 카메라 위치를 내가 컨트롤 하는 플레이어에 맞추는 작업
    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }


    // 키보드 입력
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.UP;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.DOWN;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.LEFT;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.RIGHT;
        }
        else
        {
            Dir = MoveDir.NONE;
        }
    }



}
