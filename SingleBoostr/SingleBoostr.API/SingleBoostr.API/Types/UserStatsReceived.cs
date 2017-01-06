﻿using System.Runtime.InteropServices;

namespace SingleBoostr.API.Types
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct UserStatsReceived
  {
    public ulong m_nGameID;
    public int m_eResult;
  }
}
