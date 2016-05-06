using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bewander.DAL;
using Bewander.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Text;

namespace XXX.Controllers
{
    public class ReviewsController : Controller
    {
        private XXXContext db = new XXXContext();
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }


        // -- INDEX --
        public ActionResult Index()
        {
            return View();
            //return View(db.Reviews.ToList());
        }

        public string GetReviewSummaries(string placeID)
        {
            StringBuilder summaryStringBuilder = new StringBuilder();

            List<Review> reviewList = db.Reviews
                                     .Where(pl => pl.GoogleID == placeID)
                                     .OrderByDescending(pl => pl.DatePosted)
                                     .Take(7)
                                     .ToList();

            if (reviewList.Count == 0)
            {
                summaryStringBuilder.Append("<p>There are no reviews for this location.</p>");
            }
            else {
                summaryStringBuilder.Append("<table>");
                foreach (Review review in reviewList)
                {
                    summaryStringBuilder.Append("<tr><td class='summaryItem'><a href='/Reviews/Details/" + review.ReviewID + "'>");
                    summaryStringBuilder.Append(review.Title);
                    summaryStringBuilder.Append("<br><span class='summaryItemTagline'>");
                    switch (review.SubjectID)
                    {
                        case 0:
                            summaryStringBuilder.Append("Good Eats"); break;
                        case 1:
                            summaryStringBuilder.Append("Safe Stays"); break;
                        case 2:
                            summaryStringBuilder.Append("Cool Sites"); break;
                        case 3:
                            summaryStringBuilder.Append("Fun Times"); break;
                        case 4:
                            summaryStringBuilder.Append("Legit Business"); break;
                        default:
                            summaryStringBuilder.Append("Other"); break;
                    }

                    summaryStringBuilder.Append("</span></a></td></tr>");
                    summaryStringBuilder.Append("<tr class='summarySpacer'></tr>");
                }
                summaryStringBuilder.Append("</table>");
            }

            string summaryString = summaryStringBuilder.ToString();
            return (summaryString);
        }
	}
}

