using System;
using System.Collections.Generic;

namespace TheJoyOfCode.QualityTools
{
    public class MethodSignatureCollection : List<MethodSignature>
    {
        public void Add(params Type[] types)
        {
            Add(new MethodSignature(types));
        }
    }
}