using System;
using System.Collections.Generic;

namespace jcMPP.PCL.PlatformAbstractions {
    public abstract class BaseFileIO {
        public abstract List<Guid> GetAllClientFiles();
    }
}