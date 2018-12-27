using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public static class EnumExtensions
    {
        public static string GetApiName(this NsfwType ntype)
        {
            switch(ntype)
            {
                case NsfwType.Mixed:
                    return "true";
                case NsfwType.None:
                    return "false";
                case NsfwType.Only:
                    return "only";
                default:
                    return "";
            }
        }
    }
}
