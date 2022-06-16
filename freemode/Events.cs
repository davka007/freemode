using System;
using GTANetworkAPI;

namespace freemode
{
    class Events : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStarted()
        {
            mysql.InitConnection();
        }
        [ServerEvent(Event.PlayerConnected)]
        private void OnPlayerConnected(Player player)
        {
            player.SendChatMessage("Καλώς ήρθες στο σέρβερ ~g~Global Rp.");
            if(mysql.IsAccountExist(player.Name))
            {
                player.SendChatMessage("~w~Ο λογαριασμός σας είναι ήδη ~g~εγγεγραμμένος στο σέρβερ. Χρησιμοποιείστε το /login για σύνδεση.");
            }
            else
            {
                player.SendChatMessage("~w~Ο λογαριασμός σας δεν είναι ~g~εγγεγραμμένος στο σέρβερ. Χρησιμοποιείστε το /register για εγγραφή.");
            }
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", true);
        }
        

        [ServerEvent(Event.PlayerSpawn)]
        private void OnPlayerSpawn(Player player)
        {
            player.Health = 50;
            player.Armor = 50;
        }
    }
}
