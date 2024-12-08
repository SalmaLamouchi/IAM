using Microsoft.AspNetCore.Mvc;

//using BLL.IService;
//using DAL.DTO;
using DAL.Entities;
using Services.IService;
using Microsoft.AspNetCore.Cors;
using Services.DTO;
namespace API.Controllers
{
    [Route("Profil")]
    //[Authorize]
    [Produces("application/json")]

    [EnableCors("CORSPolicy")]
    [ApiController]
    public class ProfilController : ControllerBase
    {
        //private readonly IProfilService _ProfilService;
        private readonly IServiceAsync<Profil, ProfilDto> _service;
        private readonly Serilog.ILogger _logger;
        public ProfilController(IServiceAsync<Profil, ProfilDto> service,
                     Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Profil
        [Route("GetProfils")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilDto>>> GetProfils()
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

                _logger.Error("Erreur GetProfils <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("GetProfil")]
        [HttpGet]
        public async Task<ActionResult<ProfilDto>> GetProfil(int ProfilId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var usr = await _service.GetById(ProfilId).ConfigureAwait(false); // Use int

                if (usr != null)
                {
                    return Ok(usr);
                }
                else
                {
                    var showmessage = "Profil inexistante";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetProfil <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        //// GET: api/Profil/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProfilDto>> GetProfil(int id)
        //{
        //    var Profil = await _service.GetById(id);

        //    if (Profil == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Profil);
        //}

        // POST: api/Profil
        [Route("AddProfil")]
        [HttpPost]
        public async Task<ActionResult> AjoutProfil(ProfilDto Profil)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Add(Profil).ConfigureAwait(false);

                var showmessage = "Insertion effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur AjoutProfil <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        // PUT: api/Profil/5
        //[HttpPut("{id}")]
        [Route("UpdProfil")]
        [HttpPut]
        public async Task<ActionResult> ModifProfil(ProfilDto Profil)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Update(Profil).ConfigureAwait(false);

                var showmessage = "Modification effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifProfil <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("DelProfil")]
        [HttpDelete]
        public async Task<ActionResult> DeletProfil(int ProfilId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Delete(ProfilId).ConfigureAwait(false);

                var showmessage = "Profil supprimée avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur DeletProfil <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}