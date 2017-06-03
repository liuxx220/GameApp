using System;



public enum NETPROTOCOL
{

    CL_ProofAccount_REQUEST                 = 1000,
    LC_ProofAccount_RESPOND                 = 1001,
    
    CL_GETSCENEPROTOCOL_REQUEST             = 1002,
  

    CL_ENTERSCENEPROTOCOL_REQUEST           = 1004,
    
    CL_HEARTBEATPROTOCOL_REQUEST            = 1006,
   
}


public enum NETPROTOCOL_RESPOND
{
    LC_LOGINPROTOCOL_RESPOND                = 50001,
    LC_GETSCENEPROTOCOL_RESPOND             = 50002,
    LC_ENTERSCENEPROTOCOL_RESPOND           = 50003,
    LC_HEARTBEATPROTOCOL_RESPOND            = 50004,

}