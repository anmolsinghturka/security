using System;
using System.Collections;
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

namespace PicSecurityChecks.Client.Class
{

    public partial class UniqueStatements : ArrayList
    {

        public new int Add(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException("obj");
            }

            int i;
            var loopTo = Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                switch (obj.GetType().ToString().ToUpper() ?? "")
                {
                    default:
                        {
                            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(obj, this[i], false)))
                            {
                                return i;
                            }

                            break;
                        }
                }
            }
            return base.Add(obj);
        }
    }
}
