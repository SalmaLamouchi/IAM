using Microsoft.AspNetCore.Mvc;

//using BLL.IService;
//using DAL.DTO;
using DAL.Entities;
using Services.IService;
using Microsoft.AspNetCore.Cors;
using Services.DTO;
namespace API.Controllers
{
    [Route("Role")]
    //[Authorize]
    [Produces("application/json")]

    [EnableCors("CORSPolicy")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //private readonly IRoleService _RoleService;
        private readonly IServiceAsync<Role, RoleDto> _service;
        private readonly Serilog.ILogger _logger;
        public RoleController(IServiceAsync<Role, RoleDto> service,
                     Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Role
        [Route("GetRoles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
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

                _logger.Error("Erreur GetRoles <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("GetRole")]
        [HttpGet]
        public async Task<ActionResult<RoleDto>> GetRole(int RoleId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var usr = await _service.GetById(RoleId).ConfigureAwait(false); // Use int

                if (usr != null)
                {
                    return Ok(usr);
                }
                else
                {
                    var showmessage = "Role inexistante";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetRole <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        //// GET: api/Role/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<RoleDto>> GetRole(int id)
        //{
        //    var Role = await _service.GetById(id);

        //    if (Role == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Role);
        //}

        // POST: api/Role
        [Route("AddRole")]
        [HttpPost]
        public async Task<ActionResult> AjoutRole(RoleDto Role)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Add(Role).ConfigureAwait(false);

                var showmessage = "Insertion effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur AjoutRole <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        // PUT: api/Role/5
        //[HttpPut("{id}")]
        [Route("UpdRole")]
        [HttpPut]
        public async Task<ActionResult> ModifRole(RoleDto Role)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Update(Role).ConfigureAwait(false);

                var showmessage = "Modification effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifRole <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("DelRole")]
        [HttpDelete]
        public async Task<ActionResult> DeletRole(int RoleId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Delete(RoleId).ConfigureAwait(false);

                var showmessage = "Role supprimée avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur DeletRole <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }
}