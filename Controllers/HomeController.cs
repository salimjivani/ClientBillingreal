using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientBilling.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Services;

namespace ClientBilling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 

            return View();
        }

        [HttpPost]
        public JsonResult Businesslogicinputs()
        {
            ClientBillingDataContext cm = new ClientBillingDataContext();

            var businesslogicdetails = (from a in cm.BusinessLogicDetails
                                        join b in cm.BusinessLogicLabels on a.BusinessLogicLabelID equals b.ID
                                        join c in cm.BusinessLogics on b.BusinessLogicID equals c.ID
                                        orderby c.Sheet, a.Number, b.ID
                                        select new { a.Number, a.Value, b.ID, Labels = b.Value, Businesslogicid = c.ID, c.Sheet }).ToList();


            var rootobj = new List<RealRoot>();

            var sheets = businesslogicdetails.Select(xx => xx.Sheet).Distinct().ToList(); //sheets
            for (int x = 0; x < sheets.Count(); x++)
            {
                RealRoot realcodemaint = new RealRoot()
                {
                    Sheet = sheets[x],
                    Titles = new List<string>(),
                    BusinesslogicIDs = new List<int>(),
                    Details = new List<DetailModel>()
                };

                var titles = businesslogicdetails.Where(xx => xx.Sheet == sheets[x]).Select(xx => xx.Labels).Distinct().ToList(); //Titles
                for (int y = 0; y < titles.Count(); y++)
                {
                    realcodemaint.Titles.Add(titles[y]);
                }

                var blogicids = businesslogicdetails.Where(xx => xx.Sheet == sheets[x]).Select(xx => xx.ID).Distinct().ToList(); //TitleID
                for (int z = 0; z < blogicids.Count(); z++)
                {
                    realcodemaint.BusinesslogicIDs.Add(blogicids[z]);
                }
                
                var numbers = businesslogicdetails.Where(xx => xx.Sheet == sheets[x]).Select(xx => xx.Number).Distinct().ToList();
                var numbersbygroup = businesslogicdetails.Where(xx => xx.Sheet == sheets[x]).GroupBy(g => g.Number).ToList(); //Details
                List<DetailModel> DetailModels = new List<DetailModel>();
                foreach(var numb in numbersbygroup)
                {

                    var detailmodel = new DetailModel()
                    {
                        DetailID = Convert.ToInt32(numb.Key),
                        DetailModelValues = new List<DetailModelValue>()
                    };
                    var group = numb.ToList();
                    foreach(var b in group)
                    {
                        detailmodel.DetailModelValues.Add(new DetailModelValue()
                        {
                            value = b.Value
                        });
                    }
                    DetailModels.Add(detailmodel);
                }
                realcodemaint.Details = (DetailModels);

                rootobj.Add(realcodemaint);
            }

            return Json(rootobj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertBusinessLogic(List<InsertDetails> InsertDetails)
        {
            ClientBillingDataContext cm = new ClientBillingDataContext();
            
            foreach (var a in InsertDetails)
            {
                cm.insertintosample(a.Numbers, a.BusinesslogicIDs, a.Values);
            }

            //cm.insertintosample(20,999,"Salim is testing");

            return Json("text", JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}