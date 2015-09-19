using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;

namespace Api.Myfashionmarketer.Models
{
    /// <summary>
    /// Summary description for EwalletWithdrawRequest
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EwalletWithdrawRequest : System.Web.Services.WebService
    {
        UserRepository _UserRepository = new UserRepository();
        EwalletWithdrawRequestRepository _EwalletWithdrawRequestRepository = new EwalletWithdrawRequestRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void AddRequestToWithdraw(string WithdrawAmount, string PaymentMethod, string PaypalEmail, int Status, string UserID)
        {
            
            Domain.Myfashion.Domain.User _User = _UserRepository.getUsersById(Guid.Parse(UserID));
            Domain.Myfashion.Domain.EwalletWithdrawRequest _EwalletWithdrawRequest = new Domain.Myfashion.Domain.EwalletWithdrawRequest();
            _EwalletWithdrawRequest.Id = Guid.NewGuid();
            _EwalletWithdrawRequest.UserName = _User.UserName;
            _EwalletWithdrawRequest.UserEmail = _User.EmailId;
            _EwalletWithdrawRequest.PaypalEmail = PaypalEmail;
            _EwalletWithdrawRequest.PaymentMethod = PaymentMethod;
            _EwalletWithdrawRequest.Status = Status;
            _EwalletWithdrawRequest.WithdrawAmount = WithdrawAmount;
            _EwalletWithdrawRequest.UserId =Guid.Parse(UserID);
            _EwalletWithdrawRequestRepository.Add(_EwalletWithdrawRequest);

        }
      
    }
}
