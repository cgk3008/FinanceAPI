using HouseFinanceAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static HouseFinanceAPI.Models.FinanceAPI;

namespace HouseFinanceAPI.Controllers
{
    [RoutePrefix("api/Finance")]
    public class FinancesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[Route("Budget")]
        //public async Task<List<string>> GetBudget()
        //{
        //    return await db.GetBudget();
        //}
        [Route("GetAccounts")]
        public async Task<List<PersonalAccount>> GetAccounts(int hhId)
        {
            return await db.GetAccounts(hhId);
        }



        //[Route("GetBudget")]
        //public async Task<List<Budget>> GetBudget(int hhId)
        //{
        //    return await db.GetBudget(hhId);
        //}


        [Route("GetBudgetGoal")]
        public async Task<IHttpActionResult> GetBudgetGoalsBalanceJson(int budgId)
        {
            var json = JsonConvert.SerializeObject(await db.GetBudgetGoalsBalance(budgId));
            return Ok(json);
        }











        //    [Route("AccountsV2")]
        //    public IHttpActionResult GetAccounts2(int housholdId)
        //{
        //    return db.
        //}









        [Route("TransactionDetails")]
        public async Task<List<Transaction>> GetTransactionDetails(int acctId)
        {
            return await db.GetTransactionDetails(acctId);
        }

        [Route("TransactionsByHouse")]
        public async Task<List<Transaction>> GetTransactionsByHouse(int hhId)
        {
            return await db.GetTransactionDetails(hhId);
        }


        [Route("AddTransaction")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AddTransaction(int accountId, string description, DateTimeOffset date, decimal amount, bool trxType, bool isVoid, int categoryId, string userId, bool reconciled, decimal recBalance, bool isDeleted, DateTimeOffset created)
        {
            return Ok(db.AddTransaction(accountId, description, date, amount, trxType, isVoid, categoryId, userId, reconciled, recBalance, isDeleted, created));

        }


    }



}

