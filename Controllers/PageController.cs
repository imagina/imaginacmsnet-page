// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Core;
using Core.Controllers;
using Idata.Data;
using Iprofile.Helpers;
using Microsoft.AspNetCore.Mvc;
using Page.Repositories.Interfaces;
namespace Page.Controllers
{

    [Route("api/page/v1/pages")]
    [ApiController]
    public class PageController : ControllerBase<Idata.Data.Entities.Page.Page>
    {
        private readonly IPageRepository repository;
        public PageController(IPageRepository repositoryBase, IHttpContextAccessor currentContext) : base(repositoryBase, AuthHelper.AuthUser(currentContext))
        {
            repository = repositoryBase;
        }
        #region oldCode

        //[HttpGet("")]
        //public async Task<IActionResult> Index([FromQuery] UrlRequestBase? urlRequestBase)
        //{
        //    int status = 200;
        //    dynamic response;
        //    dynamic meta = new object();
        //    try
        //    {
        //        //parser
        //        await urlRequestBase.parse(this);
        //        //repository
        //        response = await repository.GetItemsBy(urlRequestBase);

        //        //get meta before transform, because meta will be lost once object is transformed
        //        meta = await ResponseBase.GetMeta(response);
        //        //transformer
        //        response = await TransformerBase.TransformCollection(response);



        //    }
        //    catch (ExceptionBase ex)
        //    {
        //        return StatusCode(ex.CodeResult, ex.CreateResponseFromException());
        //    }
        //    //reponse
        //    return StatusCode(status, await ResponseBase.Response(response, meta));
        //}




        //[HttpGet("{criteria}")]
        //public async Task<IActionResult> Show(string? criteria, [FromQuery] UrlRequestBase? urlRequestBase)
        //{
        //    int status = 200;
        //    object? response;
        //    try
        //    {
        //        await urlRequestBase.parse(this);

        //        urlRequestBase.criteria = criteria;

        //        response = await repository.GetItem(urlRequestBase);

        //        response = await TransformerBase.TransformItem(response);
        //    }
        //    catch (ExceptionBase ex)
        //    {
        //        return StatusCode(ex.CodeResult, ex.CreateResponseFromException());
        //    }
        //    return StatusCode(status, await ResponseBase.Response(response));
        //}

        //[HttpPost("")]
        //public async Task<IActionResult> Create([FromQuery] UrlRequestBase? urlRequestBase, [FromBody] BodyRequestBase? bodyRequestBase)
        //{
        //    int status = 200;
        //    object? response;

        //    try
        //    {
        //        await urlRequestBase.parse(this);

        //        await bodyRequestBase.parse();

        //        var repositoryResponse = await repository.Create(urlRequestBase, bodyRequestBase);

        //        response = await TransformerBase.TransformItem(repositoryResponse);


        //    }
        //    catch (ExceptionBase ex)
        //    {
        //        return StatusCode(ex.CodeResult, ex.CreateResponseFromException());
        //    }

        //    return StatusCode(status, await ResponseBase.Response(response));
        //}

        //[HttpPut("{criteria}")]
        //public async Task<IActionResult> Update(string? criteria, [FromQuery] UrlRequestBase? urlRequestBase, [FromBody] BodyRequestBase? bodyRequestBase)
        //{
        //    object? response;
        //    try
        //    {
        //        await urlRequestBase.parse(this);

        //        await bodyRequestBase.parse();

        //        urlRequestBase.criteria = criteria;

        //        response = await repository.UpdateBy(urlRequestBase, bodyRequestBase);

        //        response = await TransformerBase.TransformItem(response);

        //    }
        //    catch (ExceptionBase ex)
        //    {
        //        return StatusCode(ex.CodeResult, ex.CreateResponseFromException());
        //    }

        //    return Ok(ResponseBase.Response(response));
        //}

        //[HttpDelete("{criteria}")]
        //public async Task<IActionResult> Delete(string? criteria, [FromQuery] UrlRequestBase? urlRequestBase)
        //{
        //    object response;

        //    int status = 204;
        //    try
        //    {
        //        await urlRequestBase.parse(this);


        //        urlRequestBase.criteria = criteria;

        //        response = await repository.DeleteBy(urlRequestBase);
        //    }
        //    catch (ExceptionBase ex)
        //    {
        //        return StatusCode(ex.CodeResult, ex.CreateResponseFromException());
        //    }

        //    return StatusCode(status, response);
        //}
        #endregion
        [HttpGet("cms")]
        public async Task<IActionResult> Cms([FromQuery] UrlRequestBase? urlRequestBase)
        {
            int status = 200;

            return StatusCode(404, null);
        }
    }
}