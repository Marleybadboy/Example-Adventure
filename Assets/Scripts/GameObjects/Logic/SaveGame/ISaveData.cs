

using System;

namespace HCC.GameObjects.Save
{
   
    public interface ISaveData
    {
        public void Serialize();
        public void Deserialize();
    }
}
