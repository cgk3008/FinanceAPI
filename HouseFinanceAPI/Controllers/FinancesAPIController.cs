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
        /// <summary>
        /// Get all financial accounts for a household.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the budget goal balance.
        /// </summary>
        /// <param name="budgId">Budget ID</param>
        /// <returns></returns>
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

        [Route("GetAccountsByHouseForCharts")]
        public async Task<List<HouseChart>> GetAccountsByHouseForCharts(int hhId)
        {
            return await db.GetAccountsByHouseForCharts(hhId);
        }




        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="hhId"></param>
        ///// <returns></returns>
        //        [Route("GetAccountDetails")]
        //        public async Task<PersonalAccount> GetAccountDetails( int hhId)
        //        {
        //            return await db.GetAccountsBalance( hhId);
        //        }


        /// <summary>
        /// Get the account balance for a household.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <returns></returns>
        [Route("GetAccountsBalance")]
        public async Task<decimal> GetAccountsBalance(int hhId)
        {
            return await db.GetAccountsBalance(hhId);
        }

        /// <summary>
        /// List accounts by house.
        /// </summary>
        /// <param name="Household"> Name of household</param>
        /// <returns></returns>
        [Route("GetAccountsByHousehold")]
        public async Task<List<PersonalAccount>> GetAccountsByHousehold(string Household)
        {
            return await db.GetAccountsByHousehold(Household);
        }



        /// <summary>
        /// Get the budget names for a house.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <returns></returns>
        [Route("GetAllBudgets")]
        public async Task<List<Budget>> GetAllBudgets(int hhId)
        {
            return await db.GetAllBudgets(hhId);
        }



        /// <summary>
        /// Get budget detail for a house.
        /// </summary>
        /// <param name="budgId">Budget ID</param>
        /// <returns></returns>
        [Route("GetBudget")]
        public async Task<Budget> GetBudget(int budgId)
        {
            return await db.GetBudget(budgId);
        }

        /// <summary>
        /// Get the balance of the budgets for the house.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <returns></returns>
        [Route("GetBudgetGoalsBalance")]
        public async Task<decimal> GetBudgetGoalsBalance(int hhId)
        {
            return await db.GetBudgetGoalsBalance(hhId);
        }



        /// <summary>
        /// Get the house name.
        /// </summary>
        /// <param name="Id"> House ID</param>
        /// <returns></returns>
        [Route("GetHousehold")]
        public async Task<Household> GetHousehold(int Id)
        {
            return await db.GetHousehold(Id);
        }



        /// <summary>
        /// Get transaction details.
        /// </summary>
        /// <param name="acctId">Account ID</param>
        /// <returns></returns>
        [Route("TransactionDetails")]
        public async Task<List<Transaction>> GetTransactionDetails(int acctId)
        {
            return await db.GetTransactionDetails(acctId);
        }
        /// <summary>
        /// Get all transactions for a house.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <returns></returns>
        [Route("TransactionsByHouse")]
        public async Task<List<Transaction>> GetTransactionsByHouse(int hhId)
        {
            return await db.GetTransactionsByHouse(hhId);
        }

        /// <summary>
        /// Add transaction to an account.
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="description">Description of the expense or credit</param>
        /// <param name="date">Date of transaction</param>
        /// <param name="amount">Total amount</param>
        /// <param name="trxType">An expense is false and a credit is true (check box)</param>
        /// <param name="isVoid">If transaction is void, select true or check box</param>
        /// <param name="categoryId">Categories include: Food = 3, Utilities = 4, Medical = 5, Insurance = 6, Housing =7, Transportation = 8, Clothing = 9, Personal = 10, House Supplies = 11, Debt = 12, Retirement = 13, Education = 14, Savings = 15, Gifts/Donations = 16, Entertainment = 17     
        /// </param>
        /// <param name="userId">Resident ID</param>
        /// <param name="reconciled">Reviewed and approved is true, else false</param>
        /// <param name="recBalance">The balance </param>
        /// <param name="isDeleted">Deleted is true, if not deleted it is false</param>
        /// <param name="created">Date created this transaction in budget app</param>
        /// <returns></returns>
        [Route("AddTransaction")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AddTransaction(int accountId, string description, DateTimeOffset date, decimal amount, bool trxType, bool isVoid, int categoryId, string userId, bool reconciled, decimal recBalance, bool isDeleted, DateTimeOffset created)
        {
            return Ok(db.AddTransaction(accountId, description, date, amount, trxType, isVoid, categoryId, userId, reconciled, recBalance, isDeleted, created));
        }

        /// <summary>
        /// Add an account to a household.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <param name="desc">Account name</param>
        /// <param name="balance">Amount in the account</param>
        /// <param name="recBalance"></param>
        /// <param name="user"> Resident ID</param>
        /// <param name="deleted">If account deleted this is true, else false</param>
        /// <returns></returns>
        [Route("AddAccount")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AddAccount(int hhId, string desc, decimal balance, decimal recBalance, string user, bool deleted)
        {
            return Ok(db.AddAccount(hhId, desc, balance, recBalance, user, deleted));
        }

        /// <summary>
        /// Add a budget goal.
        /// </summary>
        /// <param name="hhId">Household ID</param>
        /// <param name="name">Name of the goal or budget</param>
        /// <returns></returns>
        [Route("AddBudgetGoal")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AddBudgetGoal(int hhId, string name)
        {
            return Ok(db.AddBudgetGoal(hhId, name));
        }

        /// <summary>
        /// Budget item.
        /// </summary>
        /// <param name="caty">Categories include: Food = 3, Utilities = 4, Medical = 5, Insurance = 6, Housing =7, Transportation = 8, Clothing = 9, Personal = 10, House Supplies = 11, Debt = 12, Retirement = 13, Education = 14, Savings = 15, Gifts/Donations = 16, Entertainment = 17</param>
        /// <param name="budgId">Budget ID</param>
        /// <param name="amnt">Budget goal amount</param>
        /// <returns></returns>
        [Route("AddBudgetItem")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AddBudgetItem(int caty, int budgId, decimal amnt)
        {
            return Ok(db.AddBudgetItem(caty, budgId, amnt));
        }

        /// <summary>
        /// Create household.
        /// </summary>
        /// <param name="user">Resident ID</param>
        /// <returns></returns>
        [Route("CreateHousehold")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateHosuehold(string user)
        {
            return Ok(db.CreatHousehold(user));
        }


    }



}

