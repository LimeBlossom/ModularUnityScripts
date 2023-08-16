using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchUser
{
    public string id = "";
    public string username = "";
    public bool isBroadcaster = false;
    public bool isMod = false;
    public bool isSub = false;
    public bool isGifter = false;

    public static explicit operator TwitchUser(User v)
    {
        TwitchUser n = new TwitchUser();
        n.id = v.id;
        n.username = v.username;
        n.isBroadcaster = v.isBroadcaster;
        n.isMod = v.isMod;
        n.isSub = v.isSub;
        n.isGifter = v.isGifter;
        return n;
    }
}
