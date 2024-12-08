using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.IService;

namespace API.Controllers
{
    [Route("Menu")]
    //[Authorize]
    [Produces("application/json")]

    [EnableCors("CORSPolicy")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IServiceAsync<Menu, MenuDto> _service;
        private readonly IMenuService _menuService;
        private readonly Serilog.ILogger _logger;

        public MenuController(IServiceAsync<Menu, MenuDto> service,
                     Serilog.ILogger logger
                     ,IMenuService menuService
            )
        {
            _service = service;
            _logger = logger;
            _menuService = menuService;
        }

        // GET: api/Menu
        [Route("GetMenus")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenus()
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

                _logger.Error("Erreur GetMenus <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("GetMenu")]
        [HttpGet]
        public async Task<ActionResult<MenuDto>> GetMenu(int MenuId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var usr = await _service.GetById(MenuId).ConfigureAwait(false); // Use int

                if (usr != null)
                {
                    return Ok(usr);
                }
                else
                {
                    var showmessage = "Menu inexistante";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetMenu <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        [Route("GetMenuWithRoles")]
        [HttpGet]
        public async Task<ActionResult<MenuDto>> GetMenuWithRoles(int menuId)
        {
            try
            {
                var menu = await _menuService.GetMenuWithRolesById(menuId);

                if (menu != null)
                {
                    return Ok(menu);
                }
                else
                {
                    return NotFound(new { Message = "Menu inexistante" });
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetMenuWithRoles <==> " + ex);
                return BadRequest(new { Message = "Erreur : " + ex.Message });
            }
        }


        //// GET: api/Menu/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MenuDto>> GetMenu(int id)
        //{
        //    var Menu = await _service.GetById(id);

        //    if (Menu == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Menu);
        //}

        // POST: api/Menu
        [Route("AddMenu")]
        [HttpPost]
        public async Task<ActionResult> AjoutMenu(MenuDto Menu)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Add(Menu).ConfigureAwait(false);

                var showmessage = "Insertion effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur AjoutMenu <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        // PUT: api/Menu/5
        //[HttpPut("{id}")]
        [Route("UpdMenu")]
        [HttpPut]
        public async Task<ActionResult> ModifMenu(MenuDto Menu)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Update(Menu).ConfigureAwait(false);

                var showmessage = "Modification effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifMenu <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


       



        [Route("DelMenu")]
        [HttpDelete]
        public async Task<ActionResult> DeletMenu(int MenuId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Delete(MenuId).ConfigureAwait(false);

                var showmessage = "Menu supprimée avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur DeletMenu <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        //[Route("role/{roleId}")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenuByIdRole(int roleId)
        //{
        //    var menus = await _menuService.GetMenuByIdRoleAsync(roleId);

        //    if (menus == null || !menus.Any())
        //    {
        //        return NotFound("Aucun menu trouvé pour ce rôle.");
        //    }

        //    return Ok(menus);
        //}
    }
}
