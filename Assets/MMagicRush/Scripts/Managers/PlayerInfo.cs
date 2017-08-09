using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YupiPlay.MMB {
    public class PlayerInfo {        
        public string DisplayName;
        public string Username;

        public PlayerInventory Inventory;

        public static PlayerInfo Instance {
            get {
                if (instance == null) {
                    instance = new PlayerInfo();                    
                }

                return instance;
            }

            set { }
        }

        private static PlayerInfo instance;

        public PlayerInfo() {
            Inventory = new PlayerInventory();
        }
    }
}
