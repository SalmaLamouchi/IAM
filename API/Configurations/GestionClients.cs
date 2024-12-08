//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using IdentityServer4.Models;
//using Serilog;


//namespace API.Configurations
//{
//    public class GestionClients
//    {
//        private readonly IClientsService _service;
//        private readonly ILogger _logger;

//        public GestionClients(IClientsService service, ILogger logger)
//        {
//            _service = service;
//            _logger = logger;
//        }

//        public GestionClients()
//        {

//        }

//        public IList<Clients> GetAllClients()
//        {
//            _logger.Information("Liste des clients");
//            var clients = _service.GetAllListClients();
//            return clients;
//        }
//    }
//}
