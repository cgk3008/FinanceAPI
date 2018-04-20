using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using static HouseFinanceAPI.Models.FinanceAPI;

namespace HouseFinanceAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        //SQL Get Accounts
        public async Task<List<PersonalAccount>> GetAccounts(int HouseholdId)
        {
            return await Database.SqlQuery<PersonalAccount>("GetAccount @HouseholdId", 
                new SqlParameter("HouseholdId", HouseholdId)).ToListAsync();
        }

        public async Task<PersonalAccount> GetAccountDetails(int accountId, int hhId)
        {
            return await Database.SqlQuery<PersonalAccount>("GetAccountDetails @acctId, @hhId",
                new SqlParameter("acctId", accountId),
                new SqlParameter("hhId", hhId)).FirstOrDefaultAsync();
        }

        public async Task<PersonalAccount> GetAccountsBalance( int hhId)
        {
            return await Database.SqlQuery<PersonalAccount>("GetAccountsBalance @hhId",              
                new SqlParameter("hhId", hhId)).FirstOrDefaultAsync();
        }

        public async Task<PersonalAccount> GetAccountsByHousehold(string Household)
        {
            //need to fix my stored procedure, getting all accounts not accounts for the household
            return await Database.SqlQuery<PersonalAccount>("GetAccountsByHousehold @Household",
                new SqlParameter("Household", Household)).FirstOrDefaultAsync();
        }

        public async Task<List<Budget>> GetAllBudgets(int hhId)
        {

            //This just gets names of budgets, do I really want BudgetItems which has amounts?
            return await Database.SqlQuery<Budget>("GetAllBudgets @hhId",
                new SqlParameter("hhId", hhId))./*FirstOrDefaultAsync();*/ToListAsync();
        }

        public async Task<Budget> GetBudget(int hhId)
        {
            return await Database.SqlQuery<Budget>("GetBudget@hhId",
                new SqlParameter("hhId", hhId)).FirstOrDefaultAsync();
        }

        public async Task<BudgetItem> GetBudgetGoalsBalance(int hhId)
        {

            //This stored procedure is getting food budgetgoal, I think I want to change it from hhId to BudgetItem Id
            return await Database.SqlQuery<BudgetItem>("GetBudgetGoalsBalance @hhId",
                new SqlParameter("hhId", hhId)).FirstOrDefaultAsync();
        }

        public async Task<Household> GetHousehold(int Id)
        {
            return await Database.SqlQuery<Household>("GetHousehold @Id",
                new SqlParameter("Id", Id)).FirstOrDefaultAsync();
        }

        public async Task<List<Transaction>> GetTransactionDetails(int acctId)
        {
            return await Database.SqlQuery<Transaction>("GetTransactionDetails @acctId",
                new SqlParameter("acctId", acctId)).ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByHouse(int hhId)
        {
            return await Database.SqlQuery<Transaction>("GetTransactions @hhId",
                new SqlParameter("hhId", hhId)).ToListAsync();
        }



        //SQL Add Transaction
        public async Task<int> AddTransaction(int accountId, string description, DateTimeOffset date, decimal amount, bool trxType, bool isVoid, int categoryId, string userId, bool reconciled, decimal recBalance, bool isDeleted, DateTimeOffset created)
        {
            return await Database.ExecuteSqlCommandAsync("AddTransaction @acctId, @desc, @date, @amnt, @type, @isVoid, @caty, @user, @reconciled, @recBalance, @deleted", @created,
                new SqlParameter("acctId", accountId),
                new SqlParameter("desc", description),
                new SqlParameter("date", date),
                new SqlParameter("amnt", amount),
                new SqlParameter("type", trxType),
                new SqlParameter("isVoid", isVoid),
                new SqlParameter("caty", categoryId),
                new SqlParameter("user", userId),
                new SqlParameter("reconciled", reconciled),
                new SqlParameter("recBalance", recBalance),
                new SqlParameter("deleted", isDeleted),
                new SqlParameter("created", created));
        }

        //SQL Add Account
        public int AddAccount(int hhId, string desc, decimal balance, decimal recBalance, string user, bool deleted)
        {
            return Database.ExecuteSqlCommand("AddTransaction @hhId, @desc, @balance, @recBalance, @user, @deleted",
                new SqlParameter("hhId", hhId),
                new SqlParameter("desc", desc),
                new SqlParameter("balance", balance),
                new SqlParameter("recBalance", recBalance),
                new SqlParameter("user", user),
                new SqlParameter("deleted", deleted));
        }

        //SQL Add BudgetGoal
        public int AddBudgetGoal(int hhId, string name)
        {
            return Database.ExecuteSqlCommand("AddBudgetItem @hhId, @name",
                new SqlParameter("hhId", hhId),               
                new SqlParameter("name", name));
        }

        //SQL Add BudgetItem
        public int AddBudgetItem(int caty, int budgId, decimal amnt)
        {
            return Database.ExecuteSqlCommand("AddBudgetItem @caty, @budgId, @amnt",
                new SqlParameter("caty", caty),
                new SqlParameter("budgId", budgId),               
                new SqlParameter("amnt", amnt));
        }

        //SQL Create Household(Add new house)
        public int CreatHousehold(string user)
        {
            return Database.ExecuteSqlCommand("CreateHousehold @user",
               
                new SqlParameter("user", user));
        }


    }
}