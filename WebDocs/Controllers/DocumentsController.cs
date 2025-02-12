﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebDocs.Logic;
using WebDocs.Models;

namespace WebDocs.Controllers
{
    [Authorize]
    [Route("api/documents")]
    public class DocumentsController : Controller
    {
        private readonly IDocumentsProvider docsProvider;

        public DocumentsController(IDocumentsProvider docsProvider)
        {
            this.docsProvider = docsProvider;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetDocuments()
        {
            try
            {
                var userId = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                var documents = this.docsProvider.GetDocuments(userId);

                return this.Ok(documents);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDocument(long id)
        {
            try
            {
                var userId = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                var document = await this.docsProvider.GetDocument(userId, id);

                return this.Ok(document);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDocument(long id)
        {
            try
            {
                var userId = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                await this.docsProvider.DeleteDocument(userId, id);

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.NotFound(ex);
            }
        }
    }
}
