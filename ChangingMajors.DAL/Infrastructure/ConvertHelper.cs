using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangingMajors.DAL.Infrastructure
{
    public static class ConvertHelper
    {
        public static DateTime ConvertDate(String date)
        {
            DateTime dateTime = DateTime.Now;
            DateTime.TryParse(date, out dateTime);
            return dateTime;
        }

        public static Int32 ConvertInt32(String index)
        {
            Int32 result = 0;
            Int32.TryParse(index, out result);
            return result;
        }
    }
}
