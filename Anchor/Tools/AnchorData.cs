using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Anchor.Tools
{
    [System.Serializable]
    public class AnchorData
    {
        public string name;
        public string Location;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public bool Enabled;
    }

    [System.Serializable]
    public class AnchorDataList
    {
        public List<AnchorData> Anchors;
    }
}
