using System;
using System.Web.Mvc;
using EProSeed.Lib.BLL;
using EProSeed.Models;
using System.Linq;
using EProSeed.Web.Models;
using System.Security.Claims;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected readonly IBatch _Batch;
        protected readonly IInductee _Inductee;

        private ApplicationDbContext db = new ApplicationDbContext();
        private string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private string graphResourceID = "https://graph.windows.net";

        public HomeController()
        {
            _Batch = new Batch();
            _Inductee = new Inductee();

        }

        public async Task<ActionResult> Index()
        {
            vmDashBoard objDashboard = new vmDashBoard();
            var batch = _Batch.GetAll();
            if (batch != null && batch.Any())
            {
                UserType CurrentUserType;
               // Enum.TryParse<EProSeed.Lib.BLL.UserType>(Session["UserType"].ToString(), out CurrentUserType);
               // string CurrentUserEmail = Session["UserEmailId"].ToString();

                CurrentUserType = UserType.Trainer;
                string CurrentUserEmail = "d.pasumarthi@prowareness.nl";

                GetDataBasedOnUserType(objDashboard, batch, CurrentUserType, CurrentUserEmail);
            }

            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
           
                Uri servicePointUri = new Uri(graphResourceID);
                Uri serviceRoot = new Uri(servicePointUri, tenantID);
                ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                      async () => await GetTokenForApplication());

               

                var result = await activeDirectoryClient.Users
                    .Where(u => u.ObjectId.Equals(userObjectID))
                    .ExecuteAsync();
                IUser user = result.CurrentPage.ToList().First();

              // ViewBag.Displayname = user.DisplayName;
               objDashboard.User = user;



                return View(objDashboard);

        }

        public async Task<string> GetTokenForApplication()
        {
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // get a token for the Graph without triggering any user interaction (from the cache, via multi-resource refresh token, etc)
            ClientCredential clientcred = new ClientCredential(clientId, appKey);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's database
            AuthenticationContext authenticationContext = new AuthenticationContext(aadInstance + tenantID, new ADALTokenCache(signedInUserID));
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenSilentAsync(graphResourceID, clientcred, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
            return authenticationResult.AccessToken;
        }

        private void GetDataBasedOnUserType(vmDashBoard objDashboard, System.Collections.Generic.IList<BatchModel> batch, UserType CurrentUserType, string CurrentUserEmail)
        {
                if (CurrentUserType == UserType.Trainer)
                {
                    objDashboard.BatchList = batch;
                    var LastBatchID = batch.LastOrDefault().Id;
                    var IndList = _Inductee.Get(20, 0);
                    objDashboard.InducteeList = IndList.Where(i => i.BatchID == LastBatchID).OrderByDescending(i => i.Batch.BatchDates.OrderBy(f => f.BatchDate).First()).ToList();
                }
                else if (CurrentUserType == UserType.Trainee)
                {
                    var inducteeBatchId = _Inductee.Get(CurrentUserEmail).BatchID;
                    var traineesBatch = batch.Where(B => B.Id == inducteeBatchId).Select(B => B).ToList<BatchModel>();
                    if (traineesBatch.Any())
                    {
                        objDashboard.BatchList = traineesBatch;
                        var IndList = _Inductee.Get(20, 0);
                        objDashboard.InducteeList = IndList.Where(i => i.BatchID == inducteeBatchId).OrderByDescending(i => i.Id).ToList();
                    }
                }
        }
    }
}
