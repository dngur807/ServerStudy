﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public MyPlayerController MyPlayer { get; set; }
    Dictionary<int, GameObject> m_objects = new Dictionary<int, GameObject>();

    //List<GameObject> m_objects = new List<GameObject>();

    public void AddPlayer(PlayerInfo info, bool isMe = false)
    {
        if (isMe)
        {
            GameObject go = Managers.Resource.Instantiate("Creature/MyPlayer");
            go.name = info.name;
            m_objects.Add(info.playerId, go);

            MyPlayer = go.GetComponent<MyPlayerController>();
            MyPlayer.Id = info.playerId;
            MyPlayer.PosInfo = info.posInfo;
        }
        else
        {
            GameObject go = Managers.Resource.Instantiate("Creature/Player");
            go.name = info.name;
            m_objects.Add(info.playerId, go);

            PlayerController pc = go.GetComponent<PlayerController>();
            pc.Id = info.playerId;
            pc.PosInfo = info.posInfo;
        }
    }


    public void Remove(int id)
    {
        m_objects.Remove(id);
    }

    public GameObject Find(Vector3Int cellPos)
    {
        foreach (GameObject obj in m_objects.Values)
        {
            CreatureController cc = obj.GetComponent<CreatureController>();

            if (cc == null)
                continue;

           /* if (cc.CellPos = cellPos)
                return obj;*/

        }
        return null;
    }

    public GameObject FindById(int id)
    {
        GameObject go = null;
        m_objects.TryGetValue(id, out go);
        return go;
    }

    public void Clear()
    {
        m_objects.Clear();
    }

}
