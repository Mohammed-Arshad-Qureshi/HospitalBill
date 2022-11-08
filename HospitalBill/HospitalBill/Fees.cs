using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBill
{
    public static class Fees
    {

        public static int Price(string disese)
        {
            switch (disese)
            {
                case "Fever": return 100;
                case "Malaria": return 1000;
                case "Thyphodi": return 1100;
                case "Skin Infection": return 1500;
                case "Diearea": return 500;
                case "Heart problem": return 5000;
                case "Stomach problem": return 2000;
                case "Headache": return 600;
                case "Migration": return 1300;
                default: return 0;
            }
        }
    }
}
