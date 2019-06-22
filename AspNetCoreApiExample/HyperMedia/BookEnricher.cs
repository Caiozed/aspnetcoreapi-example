using AspNetCoreApiExample.Data.VO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace AspNetCoreApiExample.HyperMedia
{
    public class BookEnricher : ObjectContentResponseEnricher<BookVO>
    {
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "/api/v1/books/";
            string link2 = string.Concat(urlHelper.ActionContext.HttpContext.Request.Scheme, "://", urlHelper.ActionContext.HttpContext.Request.Host, path, content.Id.ToString());

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.GET,
                Href = link2,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.POST,
                Href = link2,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.PUT,
                Href = link2,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.DELETE,
                Href = link2,
                Rel = RelationType.self,
                Type = "int"
            });

            return null;
        }
    }
}
