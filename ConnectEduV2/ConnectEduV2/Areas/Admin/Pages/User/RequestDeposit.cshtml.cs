using ConnectEduV2.Models;
using ConnectEduV2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ConnectEduV2.Areas.Admin.Pages.User
{
    public class RequestDepositModel : PageModel
    {

        private readonly IDepositTransaction _transaction;
        private readonly IWalletRepository _walletRepository;
        private readonly ConnectEduV1Context _context = new ConnectEduV1Context();

        public RequestDepositModel(IDepositTransaction transaction, IWalletRepository walletRepository)
        {
            _transaction = transaction;
            _walletRepository = walletRepository;
        }

        public string SearchKeyword { get; set; }
        public string Success { get; set; }
        public IPagedList<ConnectEduV2.Models.DepositTransaction> DepositTransactions { get; set; }
        public void OnGet(int pageNumber = 1, int pageSize = 5, string searchKeyword = "")
        {
           
            DepositTransactions = _context.DepositTransactions
                .Include(x => x.Wallet)
                .ThenInclude(x => x.User)
                .Include(x => x.PaymentStatus)
                .Where( x => (x.Wallet.User.Email.Contains(searchKeyword)) 
                              && (x.PaymentStatusId== 1004 || x.PaymentStatusId == 1006))
                .OrderByDescending(x => x.Date)
                .ToPagedList(pageNumber, pageSize);
            if (TempData["Success"] != null)
            {
                Success = (string)TempData["Success"];
            }
        }
        public IActionResult OnPost(int pageNumber, int id, string action)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            if (action.Equals("Accept"))
            {
                var trans = _transaction.GetSingleById(id);
                if (trans != null)
                {
                    if(trans.PaymentStatusId == 1004)
                    {
                    trans.PaymentStatusId = 1003;
                    _transaction.Update(trans);
                    _transaction.SaveChanges();
                    var wallet = _walletRepository.GetSingleById((int)trans.WalletId);
                    wallet.Amount = (decimal) (wallet.Amount + trans.Amount);
                    _walletRepository.Update(wallet);
                    _walletRepository.SaveChanges();
                    }
                    else
                    {
                        trans.PaymentStatusId = 1008;
                        _transaction.Update(trans);
                        _transaction.SaveChanges();
                        
                    }
                    

                    
                    TempData["Success"] =  "Accept";

                    return RedirectToPage("/User/RequestDeposit", new { pageNumber = pageNumber });
                }
                else
                {
                    return RedirectToPage("/User/RequestDeposit", new { pageNumber = pageNumber });
                }
            }
            else
            {
                var trans = _transaction.GetSingleById(id);
                if (trans != null)
                {
                    if (trans.PaymentStatusId == 1004)
                    { 
                        trans.PaymentStatusId = 1005;
                        _transaction.Update(trans);
                        _transaction.SaveChanges();
                    }
                    else
                    {
                        trans.PaymentStatusId = 1007;
                        _transaction.Update(trans);
                        _transaction.SaveChanges();
                        var wallet = _walletRepository.GetSingleById((int)trans.WalletId);
                        wallet.Amount = (decimal)(wallet.Amount + trans.Amount);
                        _walletRepository.Update(wallet);
                        _walletRepository.SaveChanges();
                    }
                        


                    TempData["Success"] = "Reject";

                    return RedirectToPage("/User/RequestDeposit", new { pageNumber = pageNumber });
                }
                else
                {
                    return RedirectToPage("/User/RequestDeposit", new { pageNumber = pageNumber });
                }
            }
            
            
        }
    }
}
