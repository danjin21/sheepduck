using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAccountPacketRes
{
    public bool CreateOk;
    public string Info;
}

public class CreateAccountPacketReq
{
    public string AccountName;
    public string Password;
}