using GameData;
using NetWorkingServer;

namespace NetWorking.Msg
{
    public class Msg
    {
        public Data Data;
        public Client Client;

        public Msg(Data data,Client client)
        {
            this.Data = data;
            this.Client = client;
        }
    }
}