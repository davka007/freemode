using System;
using GTANetworkAPI;

namespace freemode
{
 class Commands : Script
    {
        [Command("veh", "/veh spawn car in coordinates of player", Alias ="vehicle" )]
        private void cmd_veh(Player player, string vehname, int color1, int color2)
        {
            uint vhash = NAPI.Util.GetHashKey(vehname);
            if(vhash <=0)
            {
                player.SendChatMessage("~r~Wrong car model");
            }
            Vehicle veh = NAPI.Vehicle.CreateVehicle(vhash, player.Position, player.Heading, color1, color2);
            veh.NumberPlate = "Matthew";
            veh.Locked = false;
            veh.EngineStatus = true;
            player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
        }
        [Command("freeze", "/freeze [player name] [true/false]")]
        private void cmd_freezeplayer(Player player, Player target, bool freezestatus)
        {
            NAPI.ClientEvent.TriggerClientEvent(target, "PlayerFreeze", freezestatus);
        }

        [Command("login", "/login [κωδικός]")]
        private void cmd_login(Player player, string password)
        {
            if(Accounts.IsPlayerLoggedIn(player))
            {
                player.SendNotification("~r~You have alreay logged in.");
                return;
            }
            if(!mysql.IsAccountExist(player.Name))
            {
                player.SendNotification("~r~You have not registered yet.");
                return;
            }
            if (mysql.IsValidPassword(player.Name, password))
            {
                player.SendNotification("~r~Wrong password.");
                return;
                
            }
            Accounts account = new Accounts(player.Name, player);
            account.Login(player, false);
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);

        }
        [Command("register", "/register [κωδικός]")]
        private void cmd_register(Player player, string password)
        {
            if (Accounts.IsPlayerLoggedIn(player))
            {
                player.SendNotification("~r~You have already logged in");
                return;
            }
            if (mysql.IsAccountExist(player.Name)) 
            {
                player.SendNotification("~r~You have already registered.");
                return;
            }

            Accounts account = new Accounts(player.Name, player);
            account.Register(player.Name, password);
            NAPI.ClientEvent.TriggerClientEvent(player, "PlayerFreeze", false);
        }
    }
}
