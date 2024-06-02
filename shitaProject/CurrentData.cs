using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace shitaProject
{
    public class CurrentData
    {
        public static CurrentData Instance;
        public static CurrentData instance
        {
            get
            {
                if (Instance == null)
                    Instance = new CurrentData();
                return Instance;
            }
        }
        private CurrentData() { }

        public SimpleDTO currentUser;

        public SimpleDTO currentLocation;

        public int MaxLinesPerRow = 12;

        public void LogOut()
        {
            currentUser = null;
            currentLocation = null;
        }

        public enum UnitNames {
           קג, גרם, ליטר, מל, יחידה, מטר
        }
    }
}
