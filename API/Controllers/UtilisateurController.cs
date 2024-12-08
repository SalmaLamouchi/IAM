using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.IService;

namespace API.Controllers
{
    [Route("User")]
    //[Authorize]
    [Produces("application/json")]

    [EnableCors("CORSPolicy")]
    [ApiController]


    public class UtilisateurController : ControllerBase
    {
        //private readonly IUtilisateurService _UtilisateurService;
        private readonly IServiceAsync<Utilisateur, UtilisateurDto> _service;
        private readonly Serilog.ILogger _logger;
        
        public UtilisateurController(IServiceAsync<Utilisateur, UtilisateurDto> service,
                     Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Utilisateur
        [Route("GetUtilisateurs")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetUtilisateurs()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var lst = _service.GetAll();
                var lstUsr = lst.ToList();

                if (lstUsr.Count != 0)
                {
                    return new OkObjectResult(lstUsr);
                }
                else
                {
                    var showmessage = "Pas d'element dans la liste";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);

                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetUtilisateurs <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("GetUtilisateur")]
        [HttpGet]
        public async Task<ActionResult<UtilisateurDto>> GetUtilisateur(int UtilisateurId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var usr = await _service.GetById(UtilisateurId).ConfigureAwait(false); // Use int

                if (usr != null)
                {
                    return Ok(usr);
                }
                else
                {
                    var showmessage = "Utilisateur inexistante";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetUtilisateur <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        //// GET: api/Utilisateur/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UtilisateurDto>> GetUtilisateur(int id)
        //{
        //    var Utilisateur = await _service.GetById(id);

        //    if (Utilisateur == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Utilisateur);
        //}

        // POST: api/Utilisateur
        [Route("AddUtilisateur")]
        [HttpPost]
        public async Task<ActionResult> AjoutUtilisateur(UtilisateurDto Utilisateur)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                // Hachage du mot de passe
                if (!string.IsNullOrEmpty(Utilisateur.Motdepasse))
                {
                    Utilisateur.Motdepasse = BCrypt.Net.BCrypt.HashPassword(Utilisateur.Motdepasse);
                }

                await _service.Add(Utilisateur).ConfigureAwait(false);

                var showmessage = "Insertion effectuée avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur AjoutUtilisateur <==> " + ex.ToString());
                var showmessage = "Erreur : " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        // PUT: api/Utilisateur/5
        //[HttpPut("{id}")]
        [Route("UpdUtilisateur")]
        [HttpPut]
        public async Task<ActionResult> ModifUtilisateur(UtilisateurDto Utilisateur)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Update(Utilisateur).ConfigureAwait(false);

                var showmessage = "Modification effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifUtilisateur <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("DelUtilisateur")]
        [HttpDelete]
        public async Task<ActionResult> DeletUtilisateur(int UtilisateurId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Delete(UtilisateurId).ConfigureAwait(false);

                var showmessage = "Utilisateur supprimée avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur DeletUtilisateur <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}