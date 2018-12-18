using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using OfficeDevPnP.Core;
using Microsoft.SharePoint.Client;
using Omya.AzureApi.Models;
using System.IO;

namespace Omya.AzureApi.Controllers
{

    public class ValuesController : ApiController
    {

        #region Omya APIs

        [Route("api/PostByInfos")]
        [SwaggerOperation("PostByInfos")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public HttpResponseMessage PostByInfos(AppParam _appParam)
        {
            HttpResponseMessage response = null;
            ClientContext _context = null;
            OmyaApp _omyaapp = null;
            List<AppInfos> _appinfos = null;
            ExceptionMessage _exceptionmessage = null;
            try
            {
                _context = OmyaRepository.GetClientContext();

                Site _site = OmyaRepository.LoadSite(_context);
                Web _web = OmyaRepository.LoadWeb(_context);
                _omyaapp = OmyaRepository.GetAppObject(_context, _appParam);
                _appinfos = OmyaRepository.GetMenuItems(_context, _omyaapp);
            }
            catch (Exception ex)
            {
                Guid _guid;
                _guid = Guid.NewGuid();
                _exceptionmessage = new ExceptionMessage();
                _exceptionmessage.Corelationid = _guid.ToString();
                _exceptionmessage.Title = ex.Message;
                _exceptionmessage.StackTrace = ex.StackTrace;
                _exceptionmessage.Created = DateTime.Now;
            }
            finally
            {
                if (null != _context)
                    _context.Dispose();
            }

            if (_exceptionmessage != null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, _exceptionmessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, _appinfos);

            return response;
        }


        [Route("api/PostByPlants")]
        [SwaggerOperation("PostByPlants")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public HttpResponseMessage PostByPlants(AppParam _appParam)
        {
            HttpResponseMessage response = null;
            ClientContext _context = null;
            OmyaApp _omyaapp = null;
            List<OmyaPlants> _omyaplants = null;
            ExceptionMessage _exceptionmessage = null;
            try
            {
                _context = OmyaRepository.GetClientContext();

                Site _site = OmyaRepository.LoadSite(_context);
                Web _web = OmyaRepository.LoadWeb(_context);
                _omyaapp = OmyaRepository.GetAppObject(_context, _appParam);
                _omyaplants = OmyaRepository.GetPlants(_context, _omyaapp);
            }
            catch (Exception ex)
            {
                Guid _guid;
                _guid = Guid.NewGuid();
                _exceptionmessage = new ExceptionMessage();
                _exceptionmessage.Corelationid = _guid.ToString();
                _exceptionmessage.Title = ex.Message;
                _exceptionmessage.StackTrace = ex.StackTrace;
                _exceptionmessage.Created = DateTime.Now;
            }
            finally
            {
                if (null != _context)
                    _context.Dispose();
            }

            if (_exceptionmessage != null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, _exceptionmessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, _omyaplants);

            return response;
        }

        [Route("api/PostInfoAttachment")]
        [SwaggerOperation("PostInfoAttachment")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public HttpResponseMessage PostInfoAttachment(AppParam _appParam)
        {
            HttpResponseMessage response = null;
            ClientContext _context = null;
            OmyaApp _omyaapp = null;
            List<OmyaAttachment> _appinfoAttachments = null;
            ExceptionMessage _exceptionmessage = null;
            try
            {
                _context = OmyaRepository.GetClientContext();

                Site _site = OmyaRepository.LoadSite(_context);
                Web _web = OmyaRepository.LoadWeb(_context);
                _omyaapp = OmyaRepository.GetAppObject(_context, _appParam);
                _appinfoAttachments = OmyaRepository.GetMenuAttachment(_context, _site, _omyaapp, _appParam.AppSegment);
            }
            catch (Exception ex)
            {
                Guid _guid;
                _guid = Guid.NewGuid();
                _exceptionmessage = new ExceptionMessage();
                _exceptionmessage.Corelationid = _guid.ToString();
                _exceptionmessage.Title = ex.Message;
                _exceptionmessage.StackTrace = ex.StackTrace;
                _exceptionmessage.Created = DateTime.Now;
            }
            finally
            {
                if (null != _context)
                    _context.Dispose();
            }

            if (_exceptionmessage != null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, _exceptionmessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, _appinfoAttachments);

            return response;
        }

        [Route("api/PostPlantAttachment")]
        [SwaggerOperation("PostPlantAttachment")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public HttpResponseMessage PostPlantAttachment(AppParam _appParam)
        {
            HttpResponseMessage response = null;
            ClientContext _context = null;
            OmyaApp _omyaapp = null;
            List<OmyaAttachment> _appPlantAttachments = null;
            ExceptionMessage _exceptionmessage = null;
            try
            {
                _context = OmyaRepository.GetClientContext();

                Site _site = OmyaRepository.LoadSite(_context);
                Web _web = OmyaRepository.LoadWeb(_context);
                _omyaapp = OmyaRepository.GetAppObject(_context, _appParam);
                _appPlantAttachments = OmyaRepository.GetPlantAttachment(_context, _site, _omyaapp, _appParam.AppPlant);
            }
            catch (Exception ex)
            {
                Guid _guid;
                _guid = Guid.NewGuid();
                _exceptionmessage = new ExceptionMessage();
                _exceptionmessage.Corelationid = _guid.ToString();
                _exceptionmessage.Title = ex.Message;
                _exceptionmessage.StackTrace = ex.StackTrace;
                _exceptionmessage.Created = DateTime.Now;
            }
            finally
            {
                if (null != _context)
                    _context.Dispose();
            }

            if (_exceptionmessage != null)
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, _exceptionmessage);
            else
                response = Request.CreateResponse(HttpStatusCode.OK, _appPlantAttachments);

            return response;
        }

        #endregion

        #region Default apis

        // GET api/values
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }

        #endregion

    }
}
